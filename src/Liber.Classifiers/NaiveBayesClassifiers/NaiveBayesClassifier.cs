// NaiveBayesClassifier.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using Liber.Classifiers.FeatureExtractors;

namespace Liber.Classifiers.NaiveBayesClassifiers;

public class NaiveBayesClassifier<TItem, TLabel> where TLabel : notnull
{
    private readonly Dictionary<TLabel, double> _logPriors =
        new Dictionary<TLabel, double>();
    private readonly Dictionary<TLabel, Dictionary<Feature, double>> _logLikelihoods =
        new Dictionary<TLabel, Dictionary<Feature, double>>();
    private readonly HashSet<Feature> _features = new HashSet<Feature>();
    private readonly IFeatureExtractor<TItem, TLabel> _featureExtractor;

    public NaiveBayesClassifier(IReadOnlyCollection<TItem> items, IFeatureExtractor<TItem, TLabel> featureExtractor) :
        this(items, featureExtractor, alpha: 1, priorStrength: 1)
    { }

    public NaiveBayesClassifier(IReadOnlyCollection<TItem> items, IFeatureExtractor<TItem, TLabel> featureExtractor, double alpha, double priorStrength)
    {
        Dictionary<TLabel, int> labelCounts = items
            .GroupBy(x => featureExtractor.GetLabel(x))
            .ToDictionary(
                keySelector: x => x.Key,
                elementSelector: x => x.Count());
        int itemCount = items.Count;

        foreach (KeyValuePair<TLabel, int> entry in labelCounts)
        {
            double frequency = double.Log((double)entry.Value / itemCount);
            double uniform = double.Log(1d / labelCounts.Count);
            
            _logPriors[entry.Key] = uniform + priorStrength * (frequency - uniform);
        }

        Dictionary<TLabel, Dictionary<Feature, int>> featureCounts = labelCounts.ToDictionary(
            keySelector: x => x.Key,
            elementSelector: x => new Dictionary<Feature, int>());

        foreach (TItem item in items)
        {
            TLabel label = featureExtractor.GetLabel(item);

            foreach (Feature feature in featureExtractor.GetFeatures(item))
            {
                _features.Add(feature);

                int count = featureCounts[label].GetValueOrDefault(feature);

                featureCounts[label][feature] = count + feature.Weight;
            }
        }

        int featureCount = _features.Count;

        foreach (KeyValuePair<TLabel, Dictionary<Feature, int>> entry in featureCounts)
        {
            Dictionary<Feature, int> counts = entry.Value;
            double totalFeatures = counts.Values.Sum() + alpha * featureCount;
            TLabel label = entry.Key;

            _logLikelihoods[label] = new Dictionary<Feature, double>();

            foreach (Feature feature in _features)
            {
                double count = counts.GetValueOrDefault(feature);

                _logLikelihoods[label][feature] = double.Log((count + alpha) / totalFeatures);
            }
        }

        _featureExtractor = featureExtractor;
    }

    public IReadOnlyDictionary<TLabel, double> Score(TItem item)
    {
        IEnumerable<Feature> features = _featureExtractor.GetFeatures(item);
        Dictionary<TLabel, double> results = new Dictionary<TLabel, double>();

        foreach (KeyValuePair<TLabel, double> entry in _logPriors)
        {
            TLabel label = entry.Key;
            double score = entry.Value;

            foreach (Feature feature in features)
            {
                score += _logLikelihoods[label].GetValueOrDefault(feature) * feature.Weight;
            }

            results[label] = score;
        }

        return results;
    }

    public TLabel? Classify(TItem item, out double score)
    {
        KeyValuePair<TLabel, double> entry = Score(item).MaxBy(x => x.Value);

        score = entry.Value;

        return entry.Key;
    }
}
