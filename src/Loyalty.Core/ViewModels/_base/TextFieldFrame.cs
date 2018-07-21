using System;
using Loyalty.Core.Models;
using MvvmCross.ViewModels;

namespace Loyalty.Core.ViewModels
{
    public class TextFieldFrame : MvxViewModel
    {
        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                SetValidationResult(null);

                SetProperty(ref _text, value, nameof(Text));
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value, nameof(Error));
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value, nameof(IsError));
        }

        public TextFieldFrame()
        {
            Error = string.Empty;
            IsError = false;
        }

        internal void SetValidationResult(ValidationResult validationResult)
        {
            if (validationResult != null)
            {
                IsError = validationResult.IsError;
                Error = validationResult.Error;
            }
            else
            {
                Error = null;
                IsError = false;
            }
        }
    }
}
