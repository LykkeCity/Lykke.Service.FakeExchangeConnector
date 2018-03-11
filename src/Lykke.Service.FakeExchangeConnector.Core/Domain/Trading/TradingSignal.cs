﻿using System;
using Newtonsoft.Json;

namespace Lykke.Service.FakeExchangeConnector.Core.Domain.Trading
{
    public enum OrderType
    {
        Unknown,
        Market,
        Limit
    }

    public enum ExecType
    {
        Unknown,
        New,
        PartialFill,
        Fill,
        DoneForDay,
        Cancelled,
        Replace,
        PendingCancel,
        Stopped,
        Rejected,
        Suspended,
        PendingNew,
        Calculated,
        Expired,
        Restarted,
        PendingReplace,
        Trade,
        TradeCorrect,
        TradeCancel,
        OrderStatus
    }

    public enum TradeType
    {
        Unknown,
        Buy,
        Sell
    }

    public enum OrderCommand
    {
        Create,
        Edit,
        Cancel
    }

    public enum TimeInForce
    {
        GoodTillCancel,
        FillOrKill
    }

    public class TradingSignal
    {
        [JsonConstructor]
        public TradingSignal(
            Instrument instrument,
            string orderId, OrderCommand command, TradeType tradeType, decimal? price, decimal volume, DateTime time, OrderType orderType = OrderType.Market,
            TimeInForce timeInForce = TimeInForce.FillOrKill)
        {
            Instrument = instrument;
            
            OrderId = orderId;
            Command = command;

            TradeType = tradeType;
            Price = price;
            Volume = volume;
            Time = time;
            OrderType = orderType;
            TimeInForce = timeInForce;
        }
        
        public Instrument Instrument { get; }

        public DateTime Time { get; }

        public OrderType OrderType { get; }

        public TradeType TradeType { get; }

        public TimeInForce TimeInForce { get; }

        public decimal? Price { get; }

        public decimal Volume { get; }

        public string OrderId { get; }

        public OrderCommand Command { get; }

        public override string ToString()
        {
            return $"Id: {OrderId}, Time: {Time}, Instrument: {Instrument}, Command: {Command}, TradeType: {TradeType}, Price: {Price}, Count: {Volume}";
        }

        public bool IsTimeInThreshold(TimeSpan threshold)
        {
            var now = DateTime.UtcNow;

            return Math.Abs(now.Ticks - Time.Ticks) <= threshold.Ticks;
        }
    }
}
