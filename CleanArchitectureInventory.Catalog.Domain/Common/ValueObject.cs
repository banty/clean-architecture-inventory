﻿using System;
namespace CleanArchitectureInventory.Catalog.Domain.Common
{
    public abstract class ValueObject
    {
        public static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                return false;
            return ReferenceEquals(left, right) || left.Equals(right!); 
        }
        public static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        public abstract IEnumerable<Object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if(obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents().Select(t => t != null ? t.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
        }
    }
}

