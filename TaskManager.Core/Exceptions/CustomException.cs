﻿using System;

namespace TaskManager.Core.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message):base(message)
        {

        }
    }
}
