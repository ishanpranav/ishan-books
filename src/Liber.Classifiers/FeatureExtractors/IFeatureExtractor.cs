// IFeatureExtractor.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Liber.Classifiers.FeatureExtractors;

public interface IFeatureExtractor<TItem, TLabel>
{
    TLabel GetLabel(TItem item);
    IEnumerable<Feature> GetFeatures(TItem item);
}
