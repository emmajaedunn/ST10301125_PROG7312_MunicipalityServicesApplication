namespace MunicipalityApplicatiion.DataStructures
{
    internal sealed class Node<T>
    {
        internal T Value;
        internal Node<T>? Next;
        internal Node(T value) { Value = value; }
    }
}