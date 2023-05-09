using System;
using System.Threading.Tasks;
using System.Windows;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.AspNetCore.SignalR.Client;

namespace WpfSignalRClientApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    HubConnection connection;

    public MainWindow()
    {
        InitializeComponent();
        connection = new HubConnectionBuilder()
            .WithUrl(@"https://localhost:7115/Notifications")
            .WithAutomaticReconnect().Build();
        //https://localhost:7115/Notifications


        connection.Reconnecting += Connection_Reconnecting;
        connection.Reconnected += Connection_Reconnected;
        connection.Closed += (sender) =>
        {
            this.Dispatcher.Invoke(() =>
            {
                var newMessage = "Attempting to Reconnect";
                messages.Items.Add(newMessage);
                ButtonSendMessage.IsEnabled = false;
            });
            return Task.CompletedTask;
        };
    }

    private Task Connection_Reconnected(string? arg)
    {
        throw new NotImplementedException();
    }

    private Task Connection_Reconnecting(Exception? arg)
    {
        this.Dispatcher.Invoke(new Action(() =>
        {
            var newMessage = "Attempting to Reconnect";
            messages.Items.Add(newMessage);
        }));
        return Task.CompletedTask;
    }

    private async void ButtonConnect_Click(object sender, RoutedEventArgs e)
    {
        connection.On<string, string>("ReceiveMessage", (user, message) => 
        {
            this.Dispatcher.Invoke(() => 
            {
                var newMessage = $"{user} :{message}";
                messages.Items.Add(newMessage);
            });
        }  );

        try
        {
            await connection.StartAsync();
            messages.Items.Add("Connection Started");
            ButtonConnect.IsEnabled = false;
            ButtonSendMessage.IsEnabled = true;
        }
        catch (Exception ex)
        {

            messages.Items.Add(ex.Message);
        }


    }

    private async void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await connection.InvokeAsync("SendMessage","WPFApp",MessageInput.Text); 

        }
        catch (Exception ex)
        {

            messages.Items.Add(ex.Message);
        }
    }
} //Class End
