using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGDSDatabase.Models.AGDS;

namespace AGDSDatabase
{
    class BoolExpr
    {
        public enum BOP { LEAF, AND, OR };

        //
        //  inner state
        //

        private BOP _op;
        private BoolExpr _left;
        private BoolExpr _right;
        private List<Item> _lit;

        //
        //  private constructor
        //

        private BoolExpr(BOP op, BoolExpr left, BoolExpr right)
        {
            _op = op;
            _left = left;
            _right = right;
            _lit = new List<Item>();
        }

        private BoolExpr(List<Item> literal)
        {
            _op = BOP.LEAF;
            _left = null;
            _right = null;
            _lit = literal;
        }

        //
        //  accessor
        //

        public BOP Op
        {
            get { return _op; }
            set { _op = value; }
        }

        public BoolExpr Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public BoolExpr Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public List<Item> Lit
        {
            get { return _lit; }
            set { _lit = value; }
        }

        //
        //  public factory
        //

        public static BoolExpr CreateAnd(BoolExpr left, BoolExpr right)
        {
            return new BoolExpr(BOP.AND, left, right);
        }

        public static BoolExpr CreateOr(BoolExpr left, BoolExpr right)
        {
            return new BoolExpr(BOP.OR, left, right);
        }

        public static BoolExpr CreateBoolVar(List<Item> str)
        {
            return new BoolExpr(str);
        }

        public BoolExpr(BoolExpr other)
        {
            // No share any object on purpose
            _op = other._op;
            _left = other._left == null ? null : new BoolExpr(other._left);
            _right = other._right == null ? null : new BoolExpr(other._right);
            _lit = other.Lit;
        }

        //
        //  state checker
        //

        public Boolean IsLeaf()
        {
            return (_op == BOP.LEAF);
        }

        public Boolean IsAtomic()
        {
            return (IsLeaf() || _left.IsLeaf());
        }
    }
}
