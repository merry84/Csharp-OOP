﻿using System;

namespace CreateAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple = true)]
    public class AuthorAttribute : Attribute
    {
        public AuthorAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }
    }
}
