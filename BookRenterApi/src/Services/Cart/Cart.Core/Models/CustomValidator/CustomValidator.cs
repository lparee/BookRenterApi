using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carts.Core.Models.CustomValidator
{
    internal class CustomValidator
    {
        public class UniqueProductAttribute : ValidationAttribute
        {
            public override bool IsValid(object? value)
            {
                if (value == null)
                    throw new ArgumentException("The property can not be null.");

                var list = value as IEnumerable;
                if (list == null)
                    throw new ArgumentException("The property must be a collection.");

                var distinctValues = list.Cast<object>().Distinct();
                return distinctValues.Count() == list.Cast<object>().Count();
            }
        }
    }
}
