// LineAccountNaiveBayesClassifier.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Liber.Classifiers.FeatureExtractors;

namespace Liber.Classifiers.NaiveBayesClassifiers;

public class LineAccountNaiveBayesClassifier : NaiveBayesClassifier<Line, Guid>
{
    private static readonly LineAccountFeatureExtractor s_featureExtractor =
        new LineAccountFeatureExtractor();

    public LineAccountNaiveBayesClassifier(IReadOnlyCollection<Line> items) :
        base(items, s_featureExtractor)
    {
    }
}
