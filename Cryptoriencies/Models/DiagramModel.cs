using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.Defaults;

namespace Cryptoriencies.Models
{
	internal class DiagramModel
	{
		public ISeries[] Series { get; set; } =
	{
		new LineSeries<double>
		{
			TooltipLabelFormatter = (chartPoint) =>
				$"{(MainWindow._labels == null?"":MainWindow._labels[(int)chartPoint.SecondaryValue])}:"+ $"${chartPoint?.PrimaryValue}",
			Values = MainWindow._observablePoints,
			Fill = null
		},
		new LineSeries<ObservablePoint, LiveChartsCore.SkiaSharpView.Drawing.Geometries.RectangleGeometry>
		{
			TooltipLabelFormatter = (chartPoint) =>
				$"",
			Values = new ObservablePoint[]
			{
				new ObservablePoint(MainWindow.labelsChose,MainWindow.observablePointsChose)
			},
		}
	};
		public Axis[] XAxes { get; set; } =
	{
		new Axis
		{
			Labels = MainWindow._labels,
		},
	};

	}
}
