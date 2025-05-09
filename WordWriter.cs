﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using Word = Microsoft.Office.Interop.Word;

namespace ModuleFour.Model
{
    public class WordWriter
    {
        public void WriteToWord(
            string filePath,
            int tableIndex,
            int columnIndex,
            string data
            )
        {
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                Word.Document wordDoc = wordApp.Documents.Open(filePath);

                Word.Table table = wordDoc.Tables[tableIndex];
                if (columnIndex < 1 || columnIndex > table.Columns.Count)
                {
                    MessageBox.Show("Неправильный индекс колонки в таблице",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                int rowIndex = 3;
                if (rowIndex <= table.Rows.Count)
                {
                    table.Cell(rowIndex, columnIndex).Range.Text = data;
                }
                else
                {
                    table.Rows.Add();
                    table.Cell(rowIndex, columnIndex).Range.Text = data;
                }
                wordDoc.Save();
                wordDoc.Close();
                wordApp.Quit();

                MessageBox.Show("Результаты теста успешно записаны в таблицу Word",
                        "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (wordApp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                }
            }
        }
    }
}
