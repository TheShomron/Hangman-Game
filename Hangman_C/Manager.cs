using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Hangman_C
{
    internal class Manager
    {

        private int _lifes;
        private string[] _TheWord;
        public string[] Display;
        private string[] _WrongGuesses;

        public Manager()
        {
            _lifes=0;
            _WrongGuesses= new string[6];    //only have 6 lifes
        }




         //hangman array --------------------------------------------------------------------------------------------------------------------------
        public static BitmapImage[] _hangman = {new BitmapImage(new Uri(@"ms-appx:///Assets/man0.png")),
            new BitmapImage(new Uri(@"ms-appx:///Assets/man1.png")),  new BitmapImage(new Uri(@"ms-appx:///Assets/man2.png"))
                ,  new BitmapImage(new Uri(@"ms-appx:///Assets/man3.png")) ,  new BitmapImage(new Uri(@"ms-appx:///Assets/man4.png")),
             new BitmapImage(new Uri(@"ms-appx:///Assets/man5.png")) ,  new BitmapImage(new Uri(@"ms-appx:///Assets/man6.png")) };




        public BitmapImage GetImage() //put the pic in the uwp
        {
            return _hangman[_lifes];
        }


        //------------------------------------------------------------------------------------------------------------------------------------------------




        public void Making_Display_n_WordArray(string word) // turn the word(string) to 2 arrays of string[]
        {
            int i = -1;
            int j = -1;
            _TheWord= new string[word.Length];
            Display= new string[word.Length];

            foreach (char x in word)
            {
                i++;
                _TheWord[i]=x.ToString();
            }

            foreach (char x in word)
            {
                j++;
                Display[j]="_";
            }
        }

        public void Replace(int i) // if in right path , reval the letter in place
        {
            Display[i]=_TheWord[i];
        }

        public string Print_Display() //print display in right path
        {
            string res = "";
            for (int i = 0; i<Display.Length; i++)
            {
                res += Display[i];
            }

            return res;
        }



        public void Wrong_Path()//add life
        {
            _lifes++;

        }


        public bool If_Already_In(string UserWord) //check if already gussed
        {

            foreach (string x in _WrongGuesses) // check if user alredy gussed
            {

                if (x== null)
                {
                    break;
                }
                else
                {
                    if (x==UserWord)
                    {
                        return true;
                    }
                }
            }

            //----------------------------------------------------------------------------------------
            foreach (string x in Display) // check within the correct gusses
            {
                if (x== null)
                {
                    continue;
                }
                else
                {
                    if (x==UserWord)
                    {
                        return true;
                    }
                }
            }
            _WrongGuesses[_lifes]= UserWord;

            return false;
        }

        public bool if_Won() // if all the words reaveled
        {
            foreach (string x in Display)
            {
                if (x=="_")
                {
                    return false;
                }
            }
            return true;
        }

        public bool if_Lost()//check if life reached 6
        {
            if (_lifes==6)
            {
                return true;
            }

            return false;
        }


        public bool isRight(string userLetter)
        {
            int check = 0;

            for (int i = 0; i<Display.Length; i++)
            {
                if (Display[i] == "_")
                {
                    if (userLetter.ToLower() == _TheWord[i])
                    {
                        Replace(i);
                        check=-1;
                    }


                }
            }

            if(check==-1)
            return true;
            else
            return false;
        }
    }

}
