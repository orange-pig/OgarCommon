﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OgarCommon.WPF.LokTarMarkup
{
    public class ReadOnlyService : DependencyObject
    {
        #region IsReadOnly

        /// <summary>
        /// IsReadOnly Attached Dependency Property
        /// </summary>
        private static readonly DependencyProperty BehaviorProperty =
            DependencyProperty.RegisterAttached("IsReadOnly", typeof(bool), typeof(ReadOnlyService),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets the IsReadOnly property.
        /// </summary>
        public static bool GetIsReadOnly(DependencyObject d)
        {
            return (bool)d.GetValue(BehaviorProperty);
        }

        /// <summary>
        /// Sets the IsReadOnly property.
        /// </summary>
        public static void SetIsReadOnly(DependencyObject d, bool value)
        {
            d.SetValue(BehaviorProperty, value);
        }

        #endregion IsReadOnly
    }
}
