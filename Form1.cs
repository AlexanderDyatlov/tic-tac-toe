using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ВВРПО_5
{
    public partial class Form1 : Form
    {
        private bool isXturn = true;
        private Button[,] buttons = new Button[3, 3];

        public Form1()
        {
            InitializeComponent();
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            // Инициализация кнопок и привязка к массиву
            buttons[0, 0] = button1;
            buttons[0, 1] = button2;
            buttons[0, 2] = button3;
            buttons[1, 0] = button4;
            buttons[1, 1] = button5;
            buttons[1, 2] = button6;
            buttons[2, 0] = button7;
            buttons[2, 1] = button8;
            buttons[2, 2] = button9;

            // Привязываем один обработчик событий ко всем кнопкам игрового поля
            foreach (var button in buttons)
            {
                button.Click += ButtonClick;
            }

            // Привязываем обработчики событий к кнопкам управления
            button12.Click += NewGameClick;
            button11.Click += SaveGameClick;
            button10.Click += LoadGameClick;
        }

        // Обработчик нажатий для всех кнопок игрового поля
        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button; // Определяем, какая кнопка была нажата
            if (button.Text == "") // Проверяем, что кнопка пустая
            {
                button.Text = isXturn ? "X" : "O"; // Устанавливаем "X" или "O" в зависимости от текущего хода
                isXturn = !isXturn; // Переключаем ход
                CheckForWinnerOrDraw(); // Проверяем, есть ли победитель или ничья
            }
        }

        // Проверка на наличие победителя или ничьи
        private void CheckForWinnerOrDraw()
        {
            string[,] board = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = buttons[i, j].Text; // Получаем текущее состояние игрового поля
                }
            }

            string winner = null;

            // Проверка строк, столбцов и диагоналей на наличие победителя
            if ((board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2] && board[0, 0] != ""))
            {
                winner = board[0, 0];
            }
            else if ((board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2] && board[1, 0] != ""))
            {
                winner = board[1, 0];
            }
            else if ((board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2] && board[2, 0] != ""))
            {
                winner = board[2, 0];
            }
            else if ((board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0] && board[0, 0] != ""))
            {
                winner = board[0, 0];
            }
            else if ((board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1] && board[0, 1] != ""))
            {
                winner = board[0, 1];
            }
            else if ((board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2] && board[0, 2] != ""))
            {
                winner = board[0, 2];
            }
            else if ((board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != ""))
            {
                winner = board[0, 0];
            }
            else if ((board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != ""))
            {
                winner = board[0, 2];
            }

            if (winner != null)
            {
                MessageBox.Show("Победили " + (winner == "X" ? "крестики" : "нолики") + "!");
                ResetGame();
            }
            else if (IsBoardFull())
            {
                MessageBox.Show("Ничья!");
                ResetGame();
            }
        }

        // Проверка, заполнено ли все поле
        private bool IsBoardFull()
        {
            foreach (var button in buttons)
            {
                if (button.Text == "")
                {
                    return false; // Если есть хотя бы одна пустая клетка, возвращаем false
                }
            }
            return true; // Все клетки заполнены
        }

        // Сброс игры
        private void ResetGame()
        {
            foreach (var button in buttons)
            {
                button.Text = ""; // Очищаем текст всех кнопок
            }
            isXturn = true; // Сбрасываем ход на "X"
        }

        // Обработка события нажатия кнопки "Новая игра"
        private void NewGameClick(object sender, EventArgs e)
        {
            ResetGame(); // Сбрасываем игру
        }

        // Обработка события нажатия кнопки "Сохранить игру"
        private void SaveGameClick(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("savegame.txt"))
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        writer.WriteLine(buttons[i, j].Text); // Сохраняем состояние каждой кнопки
                    }
                }
                writer.WriteLine(isXturn); // Сохраняем текущий ход
            }
        }

        // Обработка события нажатия кнопки "Загрузить"
        private void LoadGameClick(object sender, EventArgs e)
        {
            using (StreamReader reader = new StreamReader("savegame.txt"))
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        buttons[i, j].Text = reader.ReadLine(); // Восстанавливаем состояние каждой кнопки
                    }
                }
                isXturn = bool.Parse(reader.ReadLine()); // Восстанавливаем текущий ход
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button12_Click(object sender, EventArgs e)
        {

        }
    }
}
