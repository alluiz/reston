using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace NU2RestTest
{
    class TestModelComparer : IEqualityComparer<TestModel>
    {
        public bool Equals([AllowNull] TestModel x, [AllowNull] TestModel y)
        {
            return (
                x.Id == y.Id &&
                x.Name.Equals(y.Name) &&
                x.Status == y.Status);
        }

        public int GetHashCode([DisallowNull] TestModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
