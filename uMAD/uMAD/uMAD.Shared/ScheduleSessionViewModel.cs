using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using uMAD.Data;
using uMAD.Mvvm;

namespace uMAD
{
    public class ScheduleSessionViewModel : Microsoft.Practices.Prism.Mvvm.BindableBase
    {
        private AwaitableDelegateCommand _favoriteSessionCommand;
        private AwaitableDelegateCommand _unFavoriteSessionCommand;

        public ScheduleSession CurrentScheduleSession => ScheduleSession.CurrentSession;

        public AwaitableDelegateCommand FavoriteSessionCommand
        {
            get
            {
                if (_favoriteSessionCommand == null)
                    _favoriteSessionCommand = new AwaitableDelegateCommand(FavoriteSession, CanFavoriteSession);
                return _favoriteSessionCommand;
            }

            set
            {
                _favoriteSessionCommand = value;
            }
        }

        private async Task FavoriteSession(AwaitableDelegateCommandParameter arg)
        {
            User.CurrentUser.Favorites.Add(ScheduleSession.CurrentSession);
            OnPropertyChanged(nameof(IsFavorite));
            await User.CurrentUser.SaveAsync();
            await CurrentScheduleSession.IncrementFavoriteCount();
            FavoriteSessionCommand.RaiseCanExecuteChanged();
            UnFavoriteSessionCommand.RaiseCanExecuteChanged();
        }

        private bool CanFavoriteSession(AwaitableDelegateCommandParameter arg)
        {
            if (User.CurrentUser?.Favorites == null || User.CurrentUser.Favorites.Contains(CurrentScheduleSession))
                return false;
            return true;
        }

        public bool IsFavorite => User.CurrentUser?.Favorites != null && User.CurrentUser.Favorites.Contains(ScheduleSession.CurrentSession);

        public AwaitableDelegateCommand UnFavoriteSessionCommand
        {
            get
            {
                if (_unFavoriteSessionCommand == null)
                    _unFavoriteSessionCommand = new AwaitableDelegateCommand(UnFavoriteSession, CanUnFavoriteSession);
                return _unFavoriteSessionCommand;
            }

            set
            {
                _unFavoriteSessionCommand = value;
            }
        }

        private async Task UnFavoriteSession(AwaitableDelegateCommandParameter arg)
        {
            User.CurrentUser.Favorites.Remove(ScheduleSession.CurrentSession);
            OnPropertyChanged(nameof(IsFavorite));
            await User.CurrentUser.SaveAsync();
            await CurrentScheduleSession.DecrementFavoriteCount();
            FavoriteSessionCommand.RaiseCanExecuteChanged();
            UnFavoriteSessionCommand.RaiseCanExecuteChanged();
        }

        private bool CanUnFavoriteSession(AwaitableDelegateCommandParameter arg)
        {
            if (User.CurrentUser?.Favorites == null || !User.CurrentUser.Favorites.Contains(CurrentScheduleSession))
                return false;
            return true;
        }
    }
}
