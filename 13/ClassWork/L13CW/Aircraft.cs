using System;
using System.Collections.Generic;
using System.Text;

namespace L13CW
{
    abstract class Aircraft
    {
        protected int MaxHeight { get; private set; }

        protected int CurrentHeight { get; private set; }

        public Aircraft(int maxHeight, int currentHeight)
        {
            MaxHeight = maxHeight;
            CurrentHeight = currentHeight;
        }

        public void TakeUpper(int delta)
        {
            if (delta < 0)
            {
                throw new ArgumentOutOfRangeException("Нельзя набрать отрицательную высоту");
            }

            if (CurrentHeight + delta > MaxHeight)
            {
                CurrentHeight = MaxHeight;
            }
            else
            {
                CurrentHeight = CurrentHeight + delta;
            }
        }

        public void TakeLower(int delta)
        {
            if (delta < 0)
            {
                throw new ArgumentOutOfRangeException("Нельзя сбросить на отрицательную высоту");
            }

            if (CurrentHeight - delta > 0)
            {
                CurrentHeight = CurrentHeight - delta;
            }
            else if (CurrentHeight - delta == 0)
            {
                CurrentHeight = 0;
            }
            else
            {
                throw new InvalidOperationException("Сrash!");
            }
        }

        public override string ToString()
        {
            return $"{nameof(CurrentHeight)}: {CurrentHeight}\n" +
                   $"{nameof(MaxHeight)}: {MaxHeight}";
        }
    }
}
