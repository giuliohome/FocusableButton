using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GiulioMVVM;


namespace Text2Double
{
    class MainViewModel : ViewModelBase
    {

        void RunDouble(object par)
        {
            MyPropertyB = MyPropertyA * 2; // Debug here
        }

        bool CanRunDouble(object par)
        {
            return true;
        }


        DelegateCommand doubleCmd;
        public ICommand DoubleCmd
        {
            get
            {
                if (doubleCmd is null)
                {
                    doubleCmd = new DelegateCommand(par =>
                            RunDouble(par)
                        ,
                        par => CanRunDouble(par)
                    );
                }
                return doubleCmd;
            }
        }

        private double myVarB;

        public double MyPropertyB
        {
            get { return myVarB; }
            set { 
                myVarB = value; 
                OnPropertyChanged("MyPropertyB");
                ((DelegateCommand)DoubleCmd).RaiseCanExecuteChanged();
            }
        }


        private double myVarA;
        public double MyPropertyA
        {
            get { return myVarA; }
            set { 
                myVarA = value; 
                OnPropertyChanged("MyPropertyA");
                ((DelegateCommand)DoubleCmd).RaiseCanExecuteChanged();
            }
        }

    }
}
