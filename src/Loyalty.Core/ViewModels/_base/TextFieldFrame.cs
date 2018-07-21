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
                IsError = false;

                SetProperty(ref _text, value, nameof(Text)); 
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                IsError = string.IsNullOrEmpty(value);

                SetProperty(ref _error, value, nameof(Error)); 
            }
        }

        private bool _isError;
        public bool IsError
        {
            get => _isError;
            set => SetProperty(ref _isError, value, nameof(IsError));
        }

        internal void SetValidationResult(ValidationResult sumResult)
        {
            throw new NotImplementedException();
        }
    }
}
