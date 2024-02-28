using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerDatabase
{
	internal class PlayerDatabase
	{
		static void Main()
		{
			const string CommandAddPlayer = "1";
			const string CommandDisplayAllPlayers = "2";
			const string CommandBanPlayer = "3";
			const string CommandUnbanPlayer = "4";
			const string CommandRemovePlayer = "5";
			const string CommandExit = "6";


			Database database = new Database();

			bool isProgramActive = true;

			while (isProgramActive)
			{
				Console.WriteLine("Список команд:\n" +
						$"{CommandAddPlayer} - добавить игрока\n" +
						$"{CommandDisplayAllPlayers} - вывести всех игроков\n" +
						$"{CommandBanPlayer} - забанить игрока\n" +
						$"{CommandUnbanPlayer} - разбанить игрока\n" +
						$"{CommandRemovePlayer}  - удалить игрока\n" +
						$"{CommandExit} - выход из программы");
				Console.Write("Введите команду: ");
				string input = Console.ReadLine();

				switch (input)
				{
					case CommandAddPlayer:
						database.AddPlayer();
						break;

					case CommandDisplayAllPlayers:
						database.DisplayAllPlayers();
						break;

					case CommandBanPlayer:
						database.BanPlayer();
						break;

					case CommandUnbanPlayer:
						database.UnbanPlayer();
						break;

					case CommandRemovePlayer:
						database.RemovePlayer();
						break;

					case CommandExit:
						isProgramActive = false;
						break;

					default:
						Console.WriteLine("Неизвестная команда!");
						break;
				}

				Console.Write("Нажмите любую кнопку: ");
				Console.ReadKey();
				Console.Clear();
			}
		}
	}
}

class Player
{
	public Player(int uniqueNumber, string nickName, int level = 1, bool isBanned = false)
	{
		UniqueNumber = uniqueNumber;
		NickName = nickName;
		Level = level;
		IsBanned = isBanned;
	}

	public int Level { get; private set; }
	public string NickName { get; private set; }
	public int UniqueNumber { get; private set; }
	public bool IsBanned { get; private set; }

	public void setBan()
	{
		IsBanned = true;
	}

	public void setUnban()
	{
		IsBanned = false;
	}
}

class Database
{
	private List<Player> _players;

	public Database()
	{
		_players = new List<Player>();
	}

	public void AddPlayer()
	{
		Console.Write("Введите уникальный номер игрока: ");

		if (int.TryParse(Console.ReadLine(), out int playerUniqueNumber))
		{
			if (TryGetPlayer(playerUniqueNumber, out Player player))
			{
				Console.WriteLine("Игрок с таким уникальным номером уже существует.");
			}
			else
			{
				Console.Write("Введите ник игрока: ");
				string nickname = Console.ReadLine();
				player = new Player(playerUniqueNumber, nickname);
				_players.Add(player);
				Console.WriteLine("Новый игрок добавлен.");
			}
		}
		else
		{
			Console.WriteLine("Ошибка ввода уникального номера игрока. Введите целое число.");
		}
	}

	public void BanPlayer()
	{
		Console.Write("Введите уникальный номер игрока для бана: ");

		if (int.TryParse(Console.ReadLine(), out int playerUniqueNumber))
		{
			if (TryGetPlayer(playerUniqueNumber, out Player playerToBan))
			{
				playerToBan.setBan();
				Console.WriteLine("Игрок с указаннными уникальным номером забанен");
			}
			else
			{
				Console.WriteLine("Игрок с указанным уникальным номером не найден.");
			}
		}
		else
		{
			Console.WriteLine("Ошибка ввода уникального номера игрока. Введите целое число.");
		}
	}

	public void UnbanPlayer()
	{
		Console.Write("Введите уникальный номер игрока для разбана: ");

		if (int.TryParse(Console.ReadLine(), out int playerUniqueNumber))
		{
			if (TryGetPlayer(playerUniqueNumber, out Player playerToUnban))
			{
				playerToUnban.setUnban();
				Console.WriteLine("Игрок с указаннными уникальным номером разбанен");
			}
			else
			{
				Console.WriteLine("Игрок с указанным уникальным номером не найден.");
			}
		}
		else
		{
			Console.WriteLine("Ошибка ввода уникального номера игрока. Введите целое число.");
		}
	}

	public void RemovePlayer()
	{
		Console.Write("Введите уникальный номер игрока для удаления: ");

		if (int.TryParse(Console.ReadLine(), out int playerUniqueNumber))
		{
			if (TryGetPlayer(playerUniqueNumber, out Player playerToRemove))
			{
				_players.Remove(playerToRemove);
				Console.WriteLine("Игрок с указаннными уникальным номером удалён");
			}
			else
			{
				Console.WriteLine("Игрок с указанным уникальным номером не найден.");
			}
		}
		else
		{
			Console.WriteLine("Ошибка ввода уникального номера игрока. Введите целое число.");
		}
	}

	public void DisplayAllPlayers()
	{
		if (_players.Count > 0)
		{
			foreach (var player in _players)
			{
				Console.WriteLine($"Уникальный номер: {player.UniqueNumber}, Ник: {player.NickName}, Уровень: {player.Level}, Забанен: {player.IsBanned}");
			}
		}
		else
		{
			Console.WriteLine("База данных пуста.");
		}
	}

	private bool TryGetPlayer(int playerUniqueNumber, out Player findPlayer)
	{
		findPlayer = _players.Find(player => player.UniqueNumber == playerUniqueNumber);
		return findPlayer != null;
	}
}