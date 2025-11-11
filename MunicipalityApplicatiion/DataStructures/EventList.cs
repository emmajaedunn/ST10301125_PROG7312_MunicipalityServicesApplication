using System;
using System.Collections.Generic;
using MunicipalityApplicatiion.Models;

namespace MunicipalityApplicatiion.DataStructures
{
    public class EventList
    {
        // Store events by date
        private SortedDictionary<DateTime, List<EventItem>> eventsByDate = new SortedDictionary<DateTime, List<EventItem>>();

        // Unique categories using a Hashset
        private HashSet<string> categories = new HashSet<string>();

        // Manage upcoming events
        private Stack<EventItem> recentlyViewed = new Stack<EventItem>();
        private Queue<EventItem> upcomingEvents = new Queue<EventItem>();

        public void AddEvent(EventItem newEvent)
        {
            if (!eventsByDate.ContainsKey(newEvent.EventDate))
                eventsByDate[newEvent.EventDate] = new List<EventItem>();

            eventsByDate[newEvent.EventDate].Add(newEvent);
            categories.Add(newEvent.EventCategory);
            upcomingEvents.Enqueue(newEvent);
        }

        public List<EventItem> SearchEvents(string? category = null, DateTime? date = null)
        {
            List<EventItem> results = new List<EventItem>();

            foreach (var dateKey in eventsByDate.Keys)
            {
                foreach (var ev in eventsByDate[dateKey])
                {
                    bool matchCategory = string.IsNullOrEmpty(category) || ev.EventCategory.Equals(category, StringComparison.OrdinalIgnoreCase);
                    bool matchDate = !date.HasValue || ev.EventDate.Date == date.Value.Date;

                    if (matchCategory && matchDate)
                        results.Add(ev);
                }
            }

            return results;
        }

        public List<EventItem> GetUpcomingEvents()
        {
            return new List<EventItem>(upcomingEvents);
        }

        public void MarkAsViewed(EventItem ev)
        {
            recentlyViewed.Push(ev);
        }

        public List<EventItem> GetRecommendations()
        {
            if (recentlyViewed.Count == 0)
                return new List<EventItem>();

            var lastViewed = recentlyViewed.Peek();
            return SearchEvents(lastViewed.EventCategory);
        }

        public HashSet<string> GetCategories()
        {
            return categories;
        }
    }
}