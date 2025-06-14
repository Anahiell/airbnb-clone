using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.Additional;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.CancelPolicy;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.HomeRule;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.Events.SafetyRule;
using Airbnb.Domain.BoundedContexts.ProductAdditionalManagement.ValueObjects;
using Airbnb.SharedKernel;

namespace Airbnb.Domain.BoundedContexts.ProductAdditionalInfoManagement.Aggregates;

public class Additional : AggregateRoot
{
    public CancelPolicy? CancelPolicy { get; private set; }
    public ICollection<HomeRule> HomeRules { get; private set; } = new List<HomeRule>();
    public ICollection<SafetyRule> SafetyRules { get; private set; } = new List<SafetyRule>();
    public int ProductId { get; private set; }
    
    public Additional() { }

    #region Aggregate Methods

    public Additional(int productId, CancelPolicy? cancelPolicy, IEnumerable<HomeRule>? homeRules = null, IEnumerable<SafetyRule>? safetyRules = null)
    {
        ProductId = productId;
        CancelPolicy = cancelPolicy;
        HomeRules = homeRules?.ToList() ?? new List<HomeRule>();
        SafetyRules = safetyRules?.ToList() ?? new List<SafetyRule>();

        RaiseEvent(new AdditionalCreatedEvent(Id, productId, CancelPolicy, HomeRules, SafetyRules));
    }

    public void Update(CancelPolicy cancelPolicy, IEnumerable<HomeRule> homeRules, IEnumerable<SafetyRule> safetyRules)
    {
        CancelPolicy = cancelPolicy;
        HomeRules = homeRules.ToList();
        SafetyRules = safetyRules.ToList();

        RaiseEvent(new AdditionalUpdatedEvent(Id, CancelPolicy, HomeRules, SafetyRules));
    }

    public void UpdateCancelPolicy(CancelPolicy cancelPolicy)
    {
        CancelPolicy = cancelPolicy;
        RaiseEvent(new CancelPolicyUpdatedEvent(Id, cancelPolicy));
    }

    public void AddHomeRule(HomeRule rule)
    {
        if (HomeRules.All(r => r.Id != rule.Id))
        {
            HomeRules.Add(rule);
            RaiseEvent(new HomeRuleCreatedEvent(Id, rule.Id, rule.Type, rule.Text));
        }
    }

    public void UpdateHomeRule(HomeRule rule)
    {
        var existing = HomeRules.FirstOrDefault(r => r.Id == rule.Id);
        if (existing != null)
        {
            HomeRules.Remove(existing);
            HomeRules.Add(rule);

            RaiseEvent(new HomeRuleUpdatedEvent(Id, rule.Id, rule.Type, rule.Text));
        }
    }

    public void RemoveHomeRule(int ruleId)
    {
        var rule = HomeRules.FirstOrDefault(r => r.Id == ruleId);
        if (rule != null)
        {
            HomeRules.Remove(rule);
            RaiseEvent(new HomeRuleDeletedEvent(Id, ruleId));
        }
    }

    public void AddSafetyRule(SafetyRule rule)
    {
        if (SafetyRules.All(r => r.Id != rule.Id))
        {
            SafetyRules.Add(rule);
            RaiseEvent(new SafetyRuleCreatedEvent(Id, rule.Id, rule.Type, rule.Label));
        }
    }

    public void UpdateSafetyRule(SafetyRule rule)
    {
        var existing = SafetyRules.FirstOrDefault(r => r.Id == rule.Id);
        if (existing != null)
        {
            SafetyRules.Remove(existing);
            SafetyRules.Add(rule);

            RaiseEvent(new SafetyRuleUpdatedEvent(Id, rule.Id, rule.Type, rule.Label));
        }
    }

    public void RemoveSafetyRule(int ruleId)
    {
        var rule = SafetyRules.FirstOrDefault(r => r.Id == ruleId);
        if (rule != null)
        {
            SafetyRules.Remove(rule);
            RaiseEvent(new SafetyRuleDeletedEvent(Id, ruleId));
        }
    }

    public void Delete()
    {
        RaiseEvent(new AdditionalDeletedEvent(Id));
    }

    #endregion

    #region Event Handling

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case AdditionalCreatedEvent e:
                OnAdditionalCreated(e);
                break;

            case AdditionalUpdatedEvent e:
                OnAdditionalUpdated(e);
                break;

            case AdditionalDeletedEvent e:
                OnAdditionalDeleted(e);
                break;

            case CancelPolicyUpdatedEvent e:
                OnCancelPolicyUpdated(e);
                break;

            case CancelPolicyDeletedEvent e:
                OnCancelPolicyDeleted(e);
                break;

            case HomeRuleCreatedEvent e:
                OnHomeRuleCreated(e);
                break;

            case HomeRuleUpdatedEvent e:
                OnHomeRuleUpdated(e);
                break;

            case HomeRuleDeletedEvent e:
                OnHomeRuleDeleted(e);
                break;

            case SafetyRuleCreatedEvent e:
                OnSafetyRuleCreated(e);
                break;

            case SafetyRuleUpdatedEvent e:
                OnSafetyRuleUpdated(e);
                break;

            case SafetyRuleDeletedEvent e:
                OnSafetyRuleDeleted(e);
                break;
        }
    }

    private void OnAdditionalCreated(AdditionalCreatedEvent e)
    {
        Id = e.AggregateId;
        CancelPolicy = e.CancelPolicy;
        HomeRules = e.HomeRules.ToList();
        SafetyRules = e.SafetyRules.ToList();
    }

    private void OnAdditionalUpdated(AdditionalUpdatedEvent e)
    {
        CancelPolicy = e.CancelPolicy;
        HomeRules = e.HomeRules.ToList();
        SafetyRules = e.SafetyRules.ToList();
    }

    private void OnAdditionalDeleted(AdditionalDeletedEvent e)
    {
        Id = e.AggregateId;
    }

    private void OnCancelPolicyUpdated(CancelPolicyUpdatedEvent e)
    {
        CancelPolicy = new CancelPolicy(e.FreeCancelationDays, e.PartCancelationDays, e.PartCancelationPercent);
    }

    private void OnCancelPolicyDeleted(CancelPolicyDeletedEvent e)
    {
        CancelPolicy = null!;
    }

    private void OnHomeRuleCreated(HomeRuleCreatedEvent e)
    {
        HomeRules.Add(new HomeRule(e.RuleId, e.Type, e.Text));
    }

    private void OnHomeRuleUpdated(HomeRuleUpdatedEvent e)
    {
        var rule = HomeRules.FirstOrDefault(r => r.Id == e.RuleId);
        if (rule != null)
        {
            HomeRules.Remove(rule);
            HomeRules.Add(new HomeRule(e.RuleId, e.Type, e.Text));
        }
    }

    private void OnHomeRuleDeleted(HomeRuleDeletedEvent e)
    {
        var rule = HomeRules.FirstOrDefault(r => r.Id == e.RuleId);
        if (rule != null)
            HomeRules.Remove(rule);
    }

    private void OnSafetyRuleCreated(SafetyRuleCreatedEvent e)
    {
        SafetyRules.Add(new SafetyRule(e.RuleId, e.Type, e.Label));
    }

    private void OnSafetyRuleUpdated(SafetyRuleUpdatedEvent e)
    {
        var rule = SafetyRules.FirstOrDefault(r => r.Id == e.RuleId);
        if (rule != null)
        {
            SafetyRules.Remove(rule);
            SafetyRules.Add(new SafetyRule(e.RuleId, e.Type, e.Label));
        }
    }

    private void OnSafetyRuleDeleted(SafetyRuleDeletedEvent e)
    {
        var rule = SafetyRules.FirstOrDefault(r => r.Id == e.RuleId);
        if (rule != null)
            SafetyRules.Remove(rule);
    }

    #endregion
}