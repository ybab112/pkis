using MyWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApp.Services
{
    public class CartService
    {
        private Dictionary<string, List<CartItem>> _userCarts = new();

        public event Action? OnChange;

        public List<CartItem> GetItems(string? userName)
        {
            if (string.IsNullOrEmpty(userName)) return new List<CartItem>();

            if (!_userCarts.ContainsKey(userName))
                _userCarts[userName] = new List<CartItem>();

            return _userCarts[userName];
        }

        
        public void AddToCart(string? userName, Shoe shoe, int size, int quantity = 1)
        {
            if (string.IsNullOrEmpty(userName)) return;

            var items = GetItems(userName);
            var existingItem = items.FirstOrDefault(i => i.Shoe.Id == shoe.Id && i.Size == size);

            if (existingItem != null)
                existingItem.Quantity += quantity;
            else
                items.Add(new CartItem { Shoe = shoe, Quantity = quantity, Size = size }); 

            NotifyStateChanged();
        }

        public void RemoveFromCart(string? userName, int shoeId)
        {
            var items = GetItems(userName);
            var item = items.FirstOrDefault(i => i.Shoe.Id == shoeId);
            if (item != null)
            {
                items.Remove(item);
                NotifyStateChanged();
            }
        }

        public void UpdateSize(string userName, int shoeId, int newSize)
        {
            var items = GetItems(userName);
            var item = items.FirstOrDefault(i => i.Shoe.Id == shoeId);
            if (item != null)
            {
                item.Size = newSize; 
                OnChange?.Invoke();
            }
        }

        public void IncreaseQuantity(string? userName, int shoeId)
        {
            var item = GetItems(userName).FirstOrDefault(i => i.Shoe.Id == shoeId);
            if (item != null)
            {
                item.Quantity++;
                NotifyStateChanged();
            }
        }

        public void DecreaseQuantity(string? userName, int shoeId)
        {
            var item = GetItems(userName).FirstOrDefault(i => i.Shoe.Id == shoeId);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
                NotifyStateChanged();
            }
        }
        public void ClearCart(string? userName)
        {
            if (!string.IsNullOrEmpty(userName) && _userCarts.ContainsKey(userName))
            {
                _userCarts[userName].Clear();
                NotifyStateChanged();
            }
        }

        public int GetTotalCount(string? userName) => GetItems(userName).Sum(i => i.Quantity);
        public decimal GetTotalPrice(string? userName) => GetItems(userName).Sum(i => i.Shoe.Price * i.Quantity);

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}