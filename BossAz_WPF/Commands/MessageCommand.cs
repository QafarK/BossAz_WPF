using System.Windows;
using System.Windows.Input;

namespace BossAzWPF.Commands;

// SingleCommand
public class MessageCommand : ICommand
{
    public string? Text { get; set; }
    public MessageCommand(string? text)
    {
        Text = text;
    }



    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return Text?.Length >= 20;
    }

    public void Execute(object? parameter)
    {
        MessageBox.Show("Hakuna Matrata");
    }
}
