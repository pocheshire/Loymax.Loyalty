using System;

namespace Loyalty.Core.Models
{
    public class GiveThanksResult
    {
        public bool Success { get; set; }

        public ValidationResult SumResult { get; set; }

        public ValidationResult CommentResult { get; set; }
    }
}
