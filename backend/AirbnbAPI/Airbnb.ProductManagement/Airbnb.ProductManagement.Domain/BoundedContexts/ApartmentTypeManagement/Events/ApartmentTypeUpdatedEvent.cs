﻿using Airbnb.Domain.BoundedContexts.PropertyTypeManagement.ValueObjects;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ApartmentTypeManagement.Events;

public class ApartmentTypeUpdatedEvent(int aggregateId, PropertyTypeEnum value) : DomainEvent(aggregateId)
{
    public PropertyTypeEnum Value { get; private set; } = value;
}