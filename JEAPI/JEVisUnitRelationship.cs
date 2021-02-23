using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisUnitRelationship
    {

        public enum Type
        {

            DIVIDE, TIMES, ALTERNATE, PLUS, MINUS, POW, ROOT
        }

        JEVisUnit getUnitA();

        JEVisUnit getUnitB();

        Type getType();

    }
}
