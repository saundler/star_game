using System;

namespace Game_Homework
{
    internal class Program
    {
        static ConsoleKey StartMenu() // Метод отвечающий за меню
        {
            ConsoleKey key;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 - 3);
            Console.Write("Здравствуйте, данной игре вы будете играть за: ");
            Console.Write("*");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 - 1);
            Console.Write("Просчитывайте все ходы, чтобы не коснуться врагов!!!: ");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 1);
            Console.Write("Ваша задача состоит в том чтобы собрать пять: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("$");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 3);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("При этом вам необходимо избегать: ");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("# ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("- Движется в рандомном напрвалении на одну клетку");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 7);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("@ ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("- Движется на одну клетку преследуя игрока");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 9);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("%");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("%%%% ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("- Движется в рандомном направлении на пять клеток");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 10);
            Console.Write("        оставляя за собой след на один ход");
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 12);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Чтобы продолжить нажмите ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("Enter");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(Console.WindowWidth / 4, Console.WindowHeight / 4 + 14);
            Console.Write("Чтобы выйти в любой момент можете нажать ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("Escape");
            Console.BackgroundColor = ConsoleColor.Blue;
            do 
            {
                Console.SetCursorPosition(0, 0);
                key = Console.ReadKey().Key;
                if(key == ConsoleKey.Escape)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                }
                Console.SetCursorPosition(0, 0);
                Console.Write(" ");
            } while(key != ConsoleKey.Enter);
            Console.Clear();
            return key;
        }
        static void GameStart(ref int playerX, ref int playerY, 
                              out int bizarreX, out int bizarreY, 
                              out int stalkerX, out int stalkerY, 
                              out int coinX, out int coinY, 
                              out int chargerX, out int chargerY, 
                              int[] chargerTrackX , int[] chargerTrackY)
        {
            Random rnd = new Random(); 

            int direction = rnd.Next(0, 4); // Выбор случаайного направления

            bool chargerOverlay; // Индикатор наложения Charger Enemy на других врагов

            Console.CursorVisible = false; 
            Console.BackgroundColor = ConsoleColor.Green; 
            Console.ForegroundColor = ConsoleColor.Black; 
            Console.Clear();

            // Ввывод игрового поля на экран: 

            Console.SetCursorPosition(0, Console.WindowHeight / 4 - 1); // Установка высоты поля
            for (int i = Console.WindowHeight / 4 - 1; i <= Console.WindowHeight / 4 * 3; i++)
            {
                for (int j = 0; j <= Console.WindowWidth / 4 * 3; j++)
                {
                    if ((j == Console.WindowWidth / 4 || j == Console.WindowWidth / 4 * 3) && i != Console.WindowHeight / 4 - 1)
                    {
                        Console.Write('|'); // Вывод боковых границ игрового поля
                    }
                    else
                    {
                        if ((i == Console.WindowHeight / 4 - 1 || i == Console.WindowHeight / 4 * 3) && j >= Console.WindowWidth / 4 && j <= Console.WindowWidth / 4 * 3)
                        {
                            Console.Write('_'); // Вывод верхних и нижних границ игрового поля
                        }
                        else
                        {
                            Console.Write(' '); // Заполнение игрового поля пустыми ячейками
                        }
                    }
                }
                Console.WriteLine("");
            }

            // Установка координат для персонажей:

            // Установка координат для Bizarre Enemy
            do
            {
                bizarreX = rnd.Next(Console.WindowWidth / 4 + 1, Console.WindowWidth / 4 * 3 - 1); //  Установка координат по абциссе
                bizarreY = rnd.Next(Console.WindowHeight / 4, Console.WindowHeight / 4 * 3); // Установка координат по ординате
            } while ((bizarreX == playerX) && (bizarreY == playerY)); // Проверка на наличие наложений

            // Установка координат для Stalker Enemy
            do
            {
                stalkerX = rnd.Next(Console.WindowWidth / 4 + 1, Console.WindowWidth / 4 * 3 - 1); // Установка координат по абциссе
                stalkerY = rnd.Next(Console.WindowHeight / 4, Console.WindowHeight / 4 * 3); // Установка координат по ординате
            } while (((stalkerX == playerX) && (stalkerY == playerY)) || ((stalkerX == bizarreX) && (stalkerY == bizarreY))); // Проверка на наличие наложений

            // Установка координат для Coin
            do
            {
                coinX = rnd.Next(Console.WindowWidth / 4 + 1, Console.WindowWidth / 4 * 3 - 1); // Установка координат по абциссе
                coinY = rnd.Next(Console.WindowHeight / 4, Console.WindowHeight / 4 * 3); // Установка координат по ординате
            } while (((coinX == playerX) && (coinY == playerY)) || ((coinX == bizarreX) && (coinY == bizarreY)) || ((coinX == stalkerX) && (coinY == stalkerY))); // Проверка на наличие наложений

            // Установка координат для Charger Enemy
            do
            {
                chargerOverlay = false;
                chargerX = rnd.Next(Console.WindowWidth / 4 + 1, Console.WindowWidth / 4 * 3 - 1); // Установка координат по абциссе
                chargerY = rnd.Next(Console.WindowHeight / 4, Console.WindowHeight / 4 * 3); // Установка координат по ординате

                // Установка координат для следа Charger Enemy 
                if (direction == 0) // Проверка направления
                {
                    for (int i = 0; i < 5; i++)
                    {
                        chargerTrackX[i] = chargerX - i; // Установка координат по абциссе
                        chargerTrackY[i] = chargerY; // Установка координат по ординате
                    }
                }
                else if (direction== 1) // Проверка направления
                {
                    for (int i = 0; i < 5; i++)
                    {
                        chargerTrackX[i] = chargerX + i; // Установка координат по абциссе
                        chargerTrackY[i] = chargerY; // setting the Y coordinate
                    }
                }
                else if (direction== 2) // Проверка направления
                {
                    for (int i = 0; i < 5; i++)
                    {
                        chargerTrackX[i] = chargerX; // Установка координат по абциссе
                        chargerTrackY[i] = chargerY - i; // Установка координат по ординате
                    }
                }
                else // Проверка направления
                {
                    for (int i = 0; i < 5; i++)
                    {
                        chargerTrackX[i] = chargerX; // Установка координат по абциссе
                        chargerTrackY[i] = chargerY + i; // Установка координат по ординате
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    Transition(ref chargerTrackX[i], ref chargerTrackY[i]); // Перемещение при переходе границ
                }

                
                for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                {
                    if (((chargerTrackX[i] == playerX) && (chargerTrackY[i] == playerY)) || ((chargerTrackX[i] == bizarreX) && (chargerTrackY[i] == bizarreY)) || ((chargerTrackX[i] == stalkerX) && (chargerTrackY[i] == stalkerY)) || ((chargerTrackX[i] == coinX) && (chargerTrackY[i] == coinY))) // Проверка на наличие наложений
                    {
                        chargerOverlay = true; 
                        break;
                    }
                }
            } while (chargerOverlay); // Проверка на наличие наложений

            // Вывод табло очков на экран:

            // Вывод количесвта очков Player
            Console.SetCursorPosition(0, 0); 
            Console.Write("Player: 0");

            // Вывод количесвта очков Enemies 
            Console.SetCursorPosition(0, 1);
            Console.Write("Enemy: 0");
            
            // Вывод всех персонажей на экран:

            // Вывод Player на экран 
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(playerX, playerY);
            Console.Write("*");

            // Вывод Bizarre Enemy на экран
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(bizarreX, bizarreY);
            Console.Write('#');

            // Вывод Stalker Enemy на экран
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(stalkerX, stalkerY);
            Console.Write('@');

            // Вывод Coin на экран
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(coinX, coinY);
            Console.Write('$');

            // Вывод Charder Enemy на экран
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(chargerX, chargerY);
            Console.Write('%');

            // Вывод следа Charger Enemy на экран
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 1; i < 5; i++)
            {
                Console.SetCursorPosition(chargerTrackX[i], chargerTrackY[i]);
                Console.Write('%');
            }

            Console.ForegroundColor = ConsoleColor.Green;
        }
        static void Player(ref int playerX, ref int playerY, ConsoleKey key)
        {
            // Стирание следа
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(" ");

            // Движение в соответсвии с нажатой клавишей:

            if (key == ConsoleKey.W || key == ConsoleKey.UpArrow)
            {
                playerY -= 1; // Движение вверх
            }
            else if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
            {
                playerY += 1; // Движение вниз
            }
            else if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
            {
                playerX -= 1; // Движение влево
            }
            else if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
            {
                playerX += 1; // Движение вправо
            }

            Transition(ref playerX, ref playerY); // Перемещение при переходе границ
            Console.ForegroundColor = ConsoleColor.Black; // Установка цвета Player 
            Console.SetCursorPosition(playerX, playerY); // Установка новых координат
            Console.Write("*"); // Вывод Player на экран
        }
        static void BizarreEnemy(ref int bizarreX, ref int bizarreY, 
                                     int stalkerX, int stalkerY, 
                                     int[] chargerTrackX, int[] chargerTrackY)
        {
            Random rnd = new Random();
            int direction;
            bool chargerOverlay;

            // Стирание следа
            Console.SetCursorPosition(bizarreX, bizarreY);
            Console.Write(" ");

            do
            {
                chargerOverlay = false;
                direction = rnd.Next(0, 4); // Выбор случайного направления

                // Движение в соответсвии с направлением:

                if (direction== 0)
                {
                    ++bizarreX; // Движение вправо
                }
                else if (direction== 1)
                {
                    --bizarreX; // Движение влево
                }
                else if (direction== 2)
                {
                    ++bizarreY; // Движение вниз
                }
                else
                {
                    --bizarreY; // Движение вверх
                }

                for (int i = 0; i < 5; i++) // Цикл выявляющий наложения для каждого элемента Charger Enemy
                {
                    if ((bizarreX == chargerTrackX[i]) && (bizarreY == chargerTrackY[i])) // Проверка на наличие наложений
                    {
                        chargerOverlay = true;
                        break;
                    }
                }
            } while (chargerOverlay || ((bizarreX == stalkerX) && (bizarreY == stalkerY))); // Проверка на наличие наложений
            
            Transition(ref bizarreX, ref bizarreY); // Перемещение при переходе границ
            Console.SetCursorPosition(bizarreX, bizarreY); // Установка координат Bizarre Enemy
            Console.ForegroundColor = ConsoleColor.White; // Установка цвета Bizarre Enemy
            Console.Write('#'); // Вывод Bizarre Enemy на экран
        }
        static void StalkerEnemy(ref int stalkerX, ref int stalkerY, 
                                     int playerX, int playerY, 
                                     out int stalkerleingthX)
        {
            // Стирание следа 
            Console.SetCursorPosition(stalkerX, stalkerY);
            Console.Write(" ");

            stalkerleingthX = Math.Min(Math.Abs(stalkerX - playerX), Math.Min(((Console.WindowWidth / 4 * 3) - stalkerX + playerX - (Console.WindowWidth / 4)), (stalkerX - (Console.WindowWidth / 4) + (Console.WindowWidth / 4 * 3) - playerX)));
            if (stalkerleingthX > 0)
            {
                if (Math.Abs(stalkerX - playerX) > ((Console.WindowWidth / 4 * 3) - stalkerX + playerX - (Console.WindowWidth / 4)))
                {
                    if ((stalkerX - (Console.WindowWidth / 4) + (Console.WindowWidth / 4 * 3) - playerX) > ((Console.WindowWidth / 4 * 3) - stalkerX + playerX - (Console.WindowWidth / 4)))
                    {
                        stalkerX++;
                        stalkerleingthX = ((Console.WindowWidth / 4 * 3) - stalkerX + playerX - (Console.WindowWidth / 4));
                    }
                    else if ((stalkerX - (Console.WindowWidth / 4) + (Console.WindowWidth / 4 * 3) - playerX) < ((Console.WindowWidth / 4 * 3) - stalkerX + playerX - (Console.WindowWidth / 4)))
                    {
                        stalkerX--;
                        stalkerleingthX = (stalkerX - (Console.WindowWidth / 4) + (Console.WindowWidth / 4 * 3) - playerX);
                    }
                }
                else
                {
                    if ((stalkerX - (Console.WindowWidth / 4) + (Console.WindowWidth / 4 * 3) - playerX) > Math.Abs(stalkerX - playerX))
                    {
                        if (stalkerX > playerX)
                        {
                            stalkerX--;
                            stalkerleingthX = Math.Abs(stalkerX - playerX);
                        }
                        else if (stalkerX < playerX)
                        {
                            stalkerX++;
                            stalkerleingthX = Math.Abs(stalkerX - playerX);
                        }
                    }
                    else if ((stalkerX - (Console.WindowWidth / 4) + (Console.WindowWidth / 4 * 3) - playerX) < Math.Abs(stalkerX - playerX))
                    {
                        stalkerX--;
                        stalkerleingthX = (stalkerX - (Console.WindowWidth / 4) + (Console.WindowWidth / 4 * 3) - playerX);
                    }
                }
            }
            else
            {
                if (Math.Abs(stalkerY - playerY) > ((Console.WindowHeight / 4 * 3) - stalkerY + playerY - (Console.WindowHeight / 4)))
                {
                    if ((stalkerY - (Console.WindowHeight / 4) + (Console.WindowHeight / 4 * 3) - playerY) > ((Console.WindowHeight / 4 * 3) - stalkerY + playerY - (Console.WindowHeight / 4)))
                    {
                        stalkerY++;
                    }
                    else if ((stalkerY - (Console.WindowHeight / 4) + (Console.WindowHeight / 4 * 3) - playerY) < ((Console.WindowHeight / 4 * 3) - stalkerY + playerY - (Console.WindowHeight / 4)))
                    {
                        stalkerY--;
                    }
                }
                else
                {
                    if ((stalkerY - (Console.WindowHeight / 4) + (Console.WindowHeight / 4 * 3) - playerY) > Math.Abs(stalkerY - playerY))
                    {
                        if (stalkerY > playerY)
                        {
                            stalkerY--;
                        }
                        else if (stalkerY < playerY)
                        {
                            stalkerY++;
                        }
                    }
                    else if ((stalkerY - (Console.WindowHeight / 4) + (Console.WindowHeight / 4 * 3) - playerY) < Math.Abs(stalkerY - playerY))
                    {
                        stalkerY--;
                    }
                }
            }

            Transition(ref stalkerX, ref stalkerY); // Перемещение при переходе границ
            Console.SetCursorPosition(stalkerX, stalkerY); // Установка координат Stalker Enemy
            Console.ForegroundColor = ConsoleColor.Red; // Установка цвета Stalker Enemy
            Console.Write('@'); // Вывод Stalker Enemy на экран

            Console.ForegroundColor = ConsoleColor.Green;
        }
        static void ChargerEnemy(ref int chargerX, ref int chargerY, int[] chargerTrackX, int[] chargerTrackY, int bizarreX, int bizarreY, int stalkerX, int stalkerY)
        {
            Random rnd = new Random();
            int direction;
            bool chargerOverlay;

            // Стирание следа
            for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
            {
                Console.SetCursorPosition(chargerTrackX[i], chargerTrackY[i]);
                Console.Write(' ');
            }

            do
            {
                chargerOverlay = false;
                direction = rnd.Next(0, 4); // Выбор случайного направления

                // Движение в соответсвии с направлением:

                if (direction == 0)
                {
                    chargerX += 5; // Движение вправо
                    for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                    {
                        chargerTrackX[i] = chargerX - i; // Установка координат для следа по абциссе
                        chargerTrackY[i] = chargerY; // Установка координат для следа по ординате
                    }
                }
                else if (direction == 1)
                {
                    chargerX -= 5; // Движение влево
                    for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                    {
                        chargerTrackX[i] = chargerX + i; // Установка координат для следа по абциссе
                        chargerTrackY[i] = chargerY; // Установка координат для следа по ординате
                    }
                }
                else if (direction == 2)
                {
                    chargerY += 5; // Движение вниз
                    for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                    {
                        chargerTrackX[i] = chargerX; // Установка координат для следа по абциссе
                        chargerTrackY[i] = chargerY - i; // Установка координат для следа по ординате
                    }
                }
                else
                {
                    chargerY -= 5; // Движение вверх
                    for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                    {
                        chargerTrackX[i] = chargerX; // Установка координат для следа по абциссе
                        chargerTrackY[i] = chargerY + i; // Установка координат для следа по ординате
                    }
                }

                for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                {
                    Transition(ref chargerTrackX[i], ref chargerTrackY[i]); // Перемещение при переходе границ
                }

                for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                {
                    if (((chargerTrackX[i] == bizarreX) && (chargerTrackY[i] == bizarreY)) || ((chargerTrackX[i] == stalkerX) && (chargerTrackY[i] == stalkerY))) // Проверка на наличие наложений
                    {
                        chargerOverlay = true;
                        break;
                    }
                }
            } while (chargerOverlay); // Проверка на наличие наложений

            Transition(ref chargerX, ref chargerY); // Перемещение при переходе границ

            Console.ForegroundColor = ConsoleColor.Magenta; // Установка цвета Charger Enemy 
            Console.SetCursorPosition(chargerX, chargerY); // Установка координат Charger Enemy
            Console.Write('%'); // Вывод Charger Enemy на экран
            Console.ForegroundColor = ConsoleColor.Gray; // Установка цвета следа Charger Enemy 
            for (int i = 1; i < 5; i++) // Перебор всех элементов Charger Enemy 
            {
                Console.SetCursorPosition(chargerTrackX[i], chargerTrackY[i]); // Установка координат Charger Enemy
                Console.Write('%'); // Вывод следа Charger Enemy на экран
            }
            Console.ForegroundColor = ConsoleColor.Green;
        }
        static void Transition(ref int x, ref int y) // Перемещение при переходе границ
        {
            if (x <= Console.WindowWidth / 4)
            {
                x = Console.WindowWidth / 4 * 3 - (Console.WindowWidth / 4 - x) - 1;
            }
            else if (x >= Console.WindowWidth / 4 * 3)
            {
                x = Console.WindowWidth / 4 + (x - Console.WindowWidth / 4 * 3) + 1;
            }
            if (y <= Console.WindowHeight / 4 - 1)
            {
                y = Console.WindowHeight / 4 * 3 - (Console.WindowHeight / 4 - 1 - y) - 1;
            }
            else if (y >= Console.WindowHeight / 4 * 3)
            {
                y = Console.WindowHeight / 4 + (y - Console.WindowHeight / 4 * 3);
            }
        }
        static void Coin(ref int coinX, ref int coinY, 
                             int playerX, int playerY, 
                             int bizarreX, int bizarreY, 
                             int stalkerX, int stalkerY, 
                             int[] chargerTrackX, int[] chargerTrackY, 
                         ref int playerCounter, ref int enemiesCounter)
        {
            bool chargerOverlay = false;
            for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
            {
                if ((chargerTrackX[i] == coinX) && (chargerTrackY[i] == coinY)) // Проверка на наличие наложений
                {
                    chargerOverlay = true; 
                    break;
                }
            }
            if ((playerX == coinX && playerY == coinY) || (bizarreX == coinX && bizarreY == coinY) || (stalkerX == coinX && stalkerY == coinY) || chargerOverlay) // Проверка на наличие наложений 
            {
                chargerOverlay = false;
                Random rnd = new Random();

                if (playerX == coinX && playerY == coinY) // Наложение Player поверх Coin
                {
                    playerCounter++;
                }
                else // Наложение Enemies поверх Coin 
                {
                    enemiesCounter++;
                }

                do
                {
                    coinX = rnd.Next(Console.WindowWidth / 4 + 1, Console.WindowWidth / 4 * 3 - 1); // Определение координат для Coin по абциссе
                    coinY = rnd.Next(Console.WindowHeight / 4, Console.WindowHeight / 4 * 3); // Определение координат для Coin по ординате
                    
                    for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
                    {
                        if ((chargerTrackX[i] == coinX) && (chargerTrackY[i] == coinY)) // Проверка на наличие наложений
                        {
                            chargerOverlay = true;
                            break;
                        }
                    }
                } while (((coinX == playerX) && (coinY == playerY)) || ((coinX == bizarreX) && (coinY == bizarreY)) || ((coinX == stalkerX) && (coinY == stalkerY)) || chargerOverlay); // Проверка на наличие наложений

                Console.ForegroundColor = ConsoleColor.Yellow; // Установка цвета Coin 
                Console.SetCursorPosition(coinX, coinY); // Установка координат для Coin
                Console.Write('$'); // Вывод следа Coin на экран

                Console.ForegroundColor = ConsoleColor.Black; // Установка цвета для табло
                Console.SetCursorPosition(8, 0); 
                Console.Write(playerCounter); // Вывод счета Player
                Console.SetCursorPosition(7, 1);
                Console.Write(enemiesCounter); // Вывод счета Enemies

                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
        static bool Result(int playerX, int playerY, 
                           int bizarreX, int bizarreY, 
                           int stalkerX, int stalkerY, 
                           int coinX, int coinY, 
                           int[] chargerTrackX, int[] chargerTrackY, 
                           int playerCounter, int enemiesCounter,
                           ref bool contine)
        {
            bool chargerOverlay = false; 
            for (int i = 0; i < 5; i++) // Перебор всех элементов Charger Enemy
            {
                if ((playerX == chargerTrackX[i]) && (playerY == chargerTrackY[i])) // Проверка на наличие наложений
                {
                    chargerOverlay = true; 
                    break;
                }
            }
            if (((playerX == bizarreX) && (playerY == bizarreY)) || ((playerX == stalkerX) && (playerY == stalkerY)) || chargerOverlay || enemiesCounter == 5 || playerCounter == 5) // Проверка на наличие наложений
            {
                // Выводы результатов:

                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 7); 
                if (playerCounter == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Player wins!");
                }
                else if (enemiesCounter == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Enemies win!");
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 4 - 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Contact with the enemy!");
                    Console.ReadKey();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 7);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("GAME OVER!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 - 3);
                    Console.Write("Вас убил  - ");
                    if (((playerX == bizarreX) && (playerY == bizarreY)))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("#");
                    }
                    else if (((playerX == stalkerX) && (playerY == stalkerY)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("@");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("%");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("%%%%");
                    }
                }

                // Отчистка поля от всех персонажей:

                Console.SetCursorPosition(playerX, playerY);
                Console.Write(" ");
                Console.SetCursorPosition(bizarreX, bizarreY);
                Console.Write(" ");
                Console.SetCursorPosition(stalkerX, stalkerY);
                Console.Write(" ");
                Console.SetCursorPosition(coinX, coinY);
                Console.Write(" ");
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(chargerTrackX[i], chargerTrackY[i]);
                    Console.Write(" ");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.ReadKey();
                return GameContine(ref contine);
            }
            return true;
        }

        static bool GameContine(ref bool contine)
        {
            ConsoleKey key;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3);
            Console.Write("Для повтора игры нажмите ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write("Enter");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 3 + 3);
            Console.Write("Чтобы выйти нажмите ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("Escape");
            Console.BackgroundColor = ConsoleColor.Blue;
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    return false;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    contine = true;
                    return true;
                }
                Console.SetCursorPosition(0, 0);
                Console.Write(" ");
            } 
        }

        static void Main()
        {
            int playerX = Console.WindowWidth / 2, // Координата Player по абциссе
                playerY = Console.WindowHeight / 2, // Координата Player по ординате
                bizarreX, // Координата Bizarre по абциссе
                bizarreY, // Координата Bizarre по ординате
                stalkerX, // Координата Stalker по абциссе
                stalkerY, // Координата Stalker по ординате
                stalkerleingthX, // Растояние между Player и Stalker по оси абцисс
                coinX, // Координата Coin по абциссе
                coinY, // Координата Coin по ординате
                playerCounter = 0, // Счетчик очков Player
                enemiesCounter = 0, // Счетчик очков Enemies
                chargerX, // Координата Charger по абциссе
                chargerY; // Координата Charger по ординате

            int[] chargerTrackX = new int[5], // Координаты для всех элементов Charger по абциссе 
                  chargerTrackY = new int[5]; // Координаты для всех элементов Charger по ординате

            bool stepResult = true, // Результат одного цикла игры
                 contine = false; // Индификатор продолжения

            if (ConsoleKey.Escape == StartMenu())
            {
                return;
            }

            GameStart(ref playerX, ref playerY,       // Метод отвечающий за начало игры
                      out bizarreX, out bizarreY,     // Задает координаты для всех персонажей 
                      out stalkerX, out stalkerY,     // Вырисовывает границы игрового поля
                      out coinX, out coinY,           
                      out chargerX, out chargerY, 
                      chargerTrackX, chargerTrackY);

            ConsoleKey key = Console.ReadKey().Key; // Индификатор нажатой клавишы

            while (key != ConsoleKey.Escape && stepResult) // Цикл отвечающий ход игры
            {
                // Метод перемещающий Player
                Player(ref playerX, ref playerY, key);

                stepResult = Result(playerX, playerY, bizarreX, bizarreY, stalkerX, stalkerY, coinX, coinY, chargerTrackX, chargerTrackY, playerCounter, enemiesCounter, ref contine);

                // Метод пермещающий Stalker
                StalkerEnemy(ref stalkerX, ref stalkerY, playerX, playerY, out stalkerleingthX);
                
                // Метод перемещающий Bizarre
                BizarreEnemy(ref bizarreX, ref bizarreY, stalkerX, stalkerY, chargerTrackX, chargerTrackY);

                // Метод перемещающий Сharger
                ChargerEnemy(ref chargerX, ref chargerY, chargerTrackX, chargerTrackY, bizarreX, bizarreY, stalkerX, stalkerY);
                
                // Метод отвечающий за подсчет очков и перемещение 
                Coin(ref coinX, ref coinY, playerX, playerY, bizarreX, bizarreY, stalkerX, stalkerY, chargerTrackX, chargerTrackY, ref playerCounter, ref enemiesCounter);

                // Метод отвечающий за результат шага
                stepResult = Result(playerX, playerY, bizarreX, bizarreY, stalkerX, stalkerY, coinX, coinY, chargerTrackX, chargerTrackY, playerCounter, enemiesCounter, ref contine); 
                
                if (contine)
                {
                    contine = false;
                    GameStart(ref playerX, ref playerY,
                              out bizarreX, out bizarreY,      
                              out stalkerX, out stalkerY,    
                              out coinX, out coinY,
                              out chargerX, out chargerY,
                              chargerTrackX, chargerTrackY);

                    key = Console.ReadKey().Key; // Инициализация нажатой клавиши
                }
                
                if(stepResult)
                {
                    key = Console.ReadKey().Key; // Инициализация нажатой клавиши
                }
            }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Clear();
        }
    }
}
