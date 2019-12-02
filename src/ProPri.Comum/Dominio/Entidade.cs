using System;

namespace ProPri.Comum.Dominio
{
    public abstract class Entidade
    {
        public Guid Id { get; }
        public DateTime DataCadastro { get; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            var objEntidade = obj as Entidade;

            if (ReferenceEquals(this, objEntidade)) return true;
            return !(objEntidade is null) && Id.Equals(objEntidade.Id);
        }

        public static bool operator ==(Entidade a, Entidade b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entidade a, Entidade b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}