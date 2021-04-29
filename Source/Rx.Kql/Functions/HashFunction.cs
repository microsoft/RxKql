// /********************************************************
// *                                                       *
// *   Copyright (C) Microsoft. All rights reserved.       *
// *   Licensed under the MIT license.                     *
// *                                                       *
// ********************************************************/

using System.Text;

namespace System.Reactive.Kql
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.HashFunction.xxHash;  // The Kusto product team hash() algorithm - fast non-crypto hash

    [Description("hash")]
    public class HashFunction : ScalarFunction
    {
        /// <summary>
        ///     Empty constructor supporting Serialization/Deserialization.  DO NOT REMOVE
        /// </summary>
        public HashFunction()
        {
        }

        public override object GetValue(IDictionary<string, object> evt)
        {
            string hashTarget = Arguments[0].GetValue(evt).ToString();

            if (string.IsNullOrEmpty(hashTarget))
            {
                return string.Empty;
            }

            // Gets the hash value
            xxHashConfig config = new xxHashConfig() { HashSizeInBits = 64 };
            var factory = xxHashFactory.Instance.Create(config);
            byte[] hashedValue = factory.ComputeHash(Encoding.UTF8.GetBytes(hashTarget)).Hash;
            long value = BitConverter.ToInt64(hashedValue, 0);

            int argCount = Arguments.Count;
            switch (argCount)
            {
                case 1:
                    return value;
                case 2:
                    long modulusValue = (long)Arguments[1].GetValue(evt);
                    return value % modulusValue;
                default:
                    return string.Empty;
            }
        }
    }
}