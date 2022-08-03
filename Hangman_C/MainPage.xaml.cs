using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Hangman_C
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    { 
        Manager Manager_1 = new Manager();
        Words Word_1 = new Words();
        string the_word;


        public MainPage()
        {
            this.InitializeComponent();


            create_game();
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            Already_Guessed_That_Letter.Text="";
            if (User_Letter.Text== "")
            {
                MessageDialog error;
                error = new MessageDialog($"Error 13 , Please Enter Input");
                await error.ShowAsync();
            }
            else
            {
                bool Right_Guess = false;
                Right_Guess = Manager_1.isRight(User_Letter.Text);


                if (Right_Guess==true)
                {

                    Display.Text = Manager_1.Print_Display();

                    if (Manager_1.if_Won())
                    {
                        MessageDialog md;
                        md = new MessageDialog($"Congratulation ,  You Won!");
                        UICommand yesCommand = new UICommand("Restart", restart);
                        UICommand noCommand = new UICommand("Exit", CloseGameCommand);
                        md.Commands.Add(yesCommand);
                        md.Commands.Add(noCommand);
                        await md.ShowAsync();
                    }
                }
                else // Right_Guess==false
                {

                    if (Manager_1.If_Already_In(User_Letter.Text))
                    {
                        Already_Guessed_That_Letter.Text="Already Guessed That Letter, Silly....";
                    }
                    else
                    {
                        Manager_1.Wrong_Path();
                        ManImage.Source=  Manager_1.GetImage();

                        Wrong_Guesses.Text+=  User_Letter.Text+ ",";


                        if (Manager_1.if_Lost())
                        {
                            MessageDialog md;
                            md = new MessageDialog($"Unfortunately The Word Was: {the_word}");
                            UICommand yesCommand = new UICommand("Restart", restart);
                            UICommand noCommand = new UICommand("im done", CloseGameCommand);
                            md.Commands.Add(yesCommand);
                            md.Commands.Add(noCommand);
                            await md.ShowAsync();

                        }
                    }

                }

                User_Letter.Text="";
            }
        }



        // KEYBOARD LIMITS
        private void User_Letter_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            User_Letter.MaxLength =1;
            args.Cancel = args.NewText.Any(c => !Char.IsLower(c)) && args.NewText.Any(c => !Char.IsUpper(c));   
        }

        public void create_game()
        {
         
            Manager_1 = new Manager();
            Word_1 = new Words();


            if (easy_btn.IsChecked == true)
                the_word=Word_1.Get_Easy_Word();
            else
                the_word=Word_1.Get_Hard_Word();


            Manager_1.Making_Display_n_WordArray(the_word);
            Display.Text = Manager_1.Print_Display();
            
            ManImage.Source=  Manager_1.GetImage();
            Wrong_Guesses.Text = "Wrong Letters-\n";

        }
        private void restart(IUICommand command)
        {
            create_game();
        }

       
        private void CloseGameCommand(IUICommand command)//exit from the app
        {
            Application.Current.Exit();
        }

        private void easy_btn_Checked(object sender, RoutedEventArgs e)
        {
           
            create_game();
        }
        private void hard_btn_Checked(object sender, RoutedEventArgs e)
        {
           
            create_game();
        }
    }
}

