using MunicipalityApplicatiion.DataStructures;
using MunicipalityApplicatiion.DataStructures.Trees;
using MunicipalityApplicatiion.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalityApplicatiion.Repositories
{
    public class ServiceRequestRepository
    {
        private readonly BinarySearchTree<string, ServiceRequest> _bstById = new();
        private readonly AvlTree<long, ServiceRequest> _avlByTimeKey = new();
        private readonly RedBlackTree<string, ServiceRequest> _rbtByTitle = new();
        private readonly PriorityQueue<ServiceRequest> _priorityQueue = new();
        private readonly Graph<string> _areaGraph = new();

        private readonly List<ServiceRequest> _all;

        // ✅ Add this event so forms can react to repository changes
        public event EventHandler? Changed;

        // Helper to safely raise the event
        private void OnChanged() => Changed?.Invoke(this, EventArgs.Empty);

        public ServiceRequestRepository(IEnumerable<ServiceRequest> requests = null)
        {
            _all = requests?.ToList() ?? new List<ServiceRequest>();
            Build();
        }

        public void Add(ServiceRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RequestId))
                throw new ArgumentException("Invalid service request.");

            _all.Add(request);
            _bstById.Insert(request.RequestId, request);
            _avlByTimeKey.Insert(TimeKey(request.CreatedAt, request.RequestId), request);
            _rbtByTitle.Insert(request.Title ?? string.Empty, request);
            _priorityQueue.Insert(request, request.Priority);

            OnChanged(); // ✅ Notify subscribers
        }

        public void Update(ServiceRequest updated)
        {
            if (updated == null || string.IsNullOrEmpty(updated.RequestId))
                throw new ArgumentException("Invalid service request.");

            var existing = _all.FirstOrDefault(x => x.RequestId == updated.RequestId);
            if (existing != null)
            {
                // Replace the old one
                _all.Remove(existing);
                _all.Add(updated);

                _priorityQueue.Clear();
                Build();

                OnChanged(); // ✅ Notify subscribers
            }
        }

        public void Delete(string requestId)
        {
            var existing = _all.FirstOrDefault(x => x.RequestId == requestId);
            if (existing != null)
            {
                _all.Remove(existing);
             
                _priorityQueue.Clear();
                Build();

                OnChanged(); // ✅ Notify subscribers
            }
        }

        private void Build()
        {
            foreach (var r in _all)
            {
                _bstById.Insert(r.RequestId, r);
                _avlByTimeKey.Insert(TimeKey(r.CreatedAt, r.RequestId), r);
                _rbtByTitle.Insert(r.Title ?? string.Empty, r);
                _priorityQueue.Insert(r, r.Priority);
            }

            var locations = _all.Select(r => r.LocationNode ?? "Unknown").Distinct().ToList();
            var indices = new Dictionary<string, int>();

            for (int i = 0; i < locations.Count; i++)
                indices[locations[i]] = _areaGraph.AddNode(locations[i]);

            for (int i = 1; i < locations.Count; i++)
            {
                int u = indices[locations[i - 1]];
                int v = indices[locations[i]];
                _areaGraph.AddUndirectedEdge(u, v, 1.0);
            }
        }

        private static long TimeKey(DateTime dt, string id)
        {
            return dt.Ticks * 1000 + (id.GetHashCode() & 0xFFF);
        }

        public bool TryFindById(string requestId, out ServiceRequest request) =>
            _bstById.TryGetValue(requestId, out request);

        public bool TryGet(string requestId, out ServiceRequest request) =>
            TryFindById(requestId, out request);

        public IEnumerable<ServiceRequest> InOrderByCreatedDate() => _avlByTimeKey.InOrder();

        public bool TryFindByTitle(string title, out ServiceRequest request) =>
            _rbtByTitle.TryGet(title ?? string.Empty, out request);

        public IEnumerable<ServiceRequest> MostUrgent(int max = 5)
        {
            var result = new List<ServiceRequest>();
            var tempQueue = _priorityQueue.Clone();

            for (int i = 0; i < max && tempQueue.Count > 0; i++)
                result.Add(tempQueue.ExtractMax());

            return result;
        }

        public IEnumerable<string> AreaBfs()
        {
            if (!_all.Any()) return Enumerable.Empty<string>();
            return _areaGraph.BfsFrom(0);
        }

        public IEnumerable<(string U, string V, double W)> AreaMst()
        {
            var edges = _areaGraph.MinimumSpanningTree();
            return edges.Select(e => (U: e.U.ToString(), V: e.V.ToString(), W: e.W));
        }

        public IEnumerable<ServiceRequest> All() => _all;
    }
}