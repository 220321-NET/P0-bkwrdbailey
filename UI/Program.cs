using UI;
using DL;
using BL;

// Connection string for DB Microsoft Azure
string connectionString = File.ReadAllText("./connectionString.txt");

DBRepository repo = new DBRepository(connectionString);
StoreBL bl = new StoreBL(repo);

new MainMenu(bl).LoginMenu();