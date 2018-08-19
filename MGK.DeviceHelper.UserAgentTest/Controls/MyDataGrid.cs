using MGK.DeviceHelper.UserAgentTest.Helpers;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MGK.DeviceHelper.UserAgentTest.Controls
{
	// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
	//
	// Step 1a) Using this custom control in a XAML file that exists in the current project.
	// Add this XmlNamespace attribute to the root element of the markup file where it is 
	// to be used:
	//
	//     xmlns:MyNamespace="clr-namespace:MGK.DeviceHelper.UserAgentTest.Controls"
	//
	//
	// Step 1b) Using this custom control in a XAML file that exists in a different project.
	// Add this XmlNamespace attribute to the root element of the markup file where it is 
	// to be used:
	//
	//     xmlns:MyNamespace="clr-namespace:MGK.DeviceHelper.UserAgentTest.Controls;assembly=MGK.DeviceHelper.UserAgentTest.Controls"
	//
	// You will also need to add a project reference from the project where the XAML file lives
	// to this project and Rebuild to avoid compilation errors:
	//
	//     Right click on the target project in the Solution Explorer and
	//     "Add Reference"->"Projects"->[Browse to and select this project]
	//
	//
	// Step 2)
	// Go ahead and use your control in the XAML file.
	//
	//     <MyNamespace:MyDataGrid/>
	//
	// This custom control was based on this two posts:
	// https://blogs.msdn.microsoft.com/vinsibal/2008/09/19/wpf-datagrid-clipboard-paste-sample/
	// https://blogs.msdn.microsoft.com/vinsibal/2008/09/25/pasting-content-to-new-rows-on-the-wpf-datagrid/
	public class MyDataGrid : DataGrid
	{
		#region Private variables and objects
		/// <summary>
		/// DependencyProperty for CanUserAddRows.
		/// </summary>
		private static readonly DependencyProperty CanUserPasteToNewRowsProperty =
			DependencyProperty.Register("CanUserPasteToNewRows",
										typeof(bool),
										typeof(MyDataGrid),
										new FrameworkPropertyMetadata(true, null, null));
		#endregion

		#region Constructors
		static MyDataGrid()
		{
			CommandManager.RegisterClassCommandBinding(
				typeof(MyDataGrid),
				new CommandBinding(
					ApplicationCommands.Paste,
					new ExecutedRoutedEventHandler(OnExecutedPaste),
					new CanExecuteRoutedEventHandler(OnCanExecutePaste)));
		}
		#endregion

		#region Properties
		/// <summary>
		/// Whether new rows can be added to the ItemsSource.
		/// </summary>
		public bool CanUserPasteToNewRows
		{
			get => (bool)GetValue(CanUserPasteToNewRowsProperty);
			set => SetValue(CanUserPasteToNewRowsProperty, value);
		}
		#endregion

		#region Private methods
		private static void OnCanExecutePaste(object source, CanExecuteRoutedEventArgs e)
		{
			((MyDataGrid)source).OnCanExecutePaste(e);
		}

		protected virtual void OnCanExecutePaste(CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = CurrentCell != null;
			e.Handled = true;
		}

		private static void OnExecutedPaste(object source, ExecutedRoutedEventArgs e)
		{
			((MyDataGrid)source).OnExecutedPaste(e);
		}

		protected virtual void OnExecutedPaste(ExecutedRoutedEventArgs e)
		{
			// parse the clipboard data
			var rowData = ClipboardHelper.Parse().ToList();
			var hasAddedNewRow = false;

			// call OnPastingCellClipboardContent for each cell
			var minRowIndex = Items.IndexOf(CurrentItem);
			var maxRowIndex = Items.Count - 1;
			var minColumnDisplayIndex = (SelectionUnit != DataGridSelectionUnit.FullRow) ? Columns.IndexOf(CurrentColumn) : 0;
			var maxColumnDisplayIndex = Columns.Count - 1;
			var rowDataIndex = 0;

			for (var i = minRowIndex;
				 i <= maxRowIndex && rowDataIndex < rowData.Count;
				 i++, rowDataIndex++)
			{
				if (CanUserPasteToNewRows && CanUserAddRows && i == maxRowIndex)
				{
					// add a new row to be pasted to
					var cv = CollectionViewSource.GetDefaultView(Items);
					var iecv = cv as IEditableCollectionView;

					if (iecv != null)
					{
						hasAddedNewRow = true;
						iecv.AddNew();

						if ((rowDataIndex + 1) < rowData.Count)
						{
							// still has more items to paste, update the maxRowIndex
							maxRowIndex = Items.Count - 1;
						}
					}
				}
				else if (i == maxRowIndex)
				{
					continue;
				}

				var columnDataIndex = 0;
				var rowDataLine = rowData[rowDataIndex].ToArray();

				for (int j = minColumnDisplayIndex;
					 j < maxColumnDisplayIndex && columnDataIndex < rowDataLine.Length;
					 j++, columnDataIndex++)
				{
					var column = ColumnFromDisplayIndex(j);
					column.OnPastingCellClipboardContent(Items[i], rowDataLine[columnDataIndex]);
				}
			}

			// update selection
			if (hasAddedNewRow)
			{
				UnselectAll();
				UnselectAllCells();
				CurrentItem = Items[minRowIndex];

				if (SelectionUnit == DataGridSelectionUnit.FullRow)
				{
					SelectedItem = Items[minRowIndex];
				}
				else if (SelectionUnit == DataGridSelectionUnit.CellOrRowHeader || SelectionUnit == DataGridSelectionUnit.Cell)
				{
					SelectedCells.Add(new DataGridCellInfo(Items[minRowIndex], Columns[minColumnDisplayIndex]));
				}
			}
		}
		#endregion
	}
}
