using System;
using System.Windows.Input;

namespace ELTE.TravelAgency.Admin.ViewModel
{
    /// <summary>
    /// Általános parancs típusa.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<Object> _execute; // a tevékenységet végrehajtó lambda-kifejezés
        private readonly Func<Object, Boolean> _canExecute; // a tevékenység feltételét ellenőző lambda-kifejezés

        /// <summary>
        /// Végrehajthatóság változásának eseménye.
        /// </summary>
        public event EventHandler CanExecuteChanged { 
            add { CommandManager.RequerySuggested += value; } 
            remove { CommandManager.RequerySuggested -= value; } 
        }

        /// <summary>
        /// Parancs létrehozása.
        /// </summary>
        /// <param name="execute">Végrehajtandó tevékenység.</param>
        public DelegateCommand(Action<Object> execute) : this(null, execute) { }

        /// <summary>
        /// Parancs létrehozása.
        /// </summary>
        /// <param name="canExecute">Végrehajthatóság feltétele.</param>
        /// <param name="execute">Végrehajtandó tevékenység.</param>
        public DelegateCommand(Func<Object, Boolean> canExecute, Action<Object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Végrehajthatóság ellenőrzése
        /// </summary>
        /// <param name="parameter">A tevékenység paramétere.</param>
        /// <returns>Igaz, ha a tevékenység végrehajtható.</returns>
        public Boolean CanExecute(Object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Tevékenység végrehajtása.
        /// </summary>
        /// <param name="parameter">A tevékenység paramétere.</param>
        public void Execute(Object parameter)
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Command execution is disabled.");
            }
            _execute(parameter);
        }
    }
}
