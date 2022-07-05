using Subscriptions.DataSource;
using System;
using Xunit;

namespace SubscriptionsUnitTests
{
    public class SubscriptionsDataSourceTests
    {
        [Fact]
        public void SourceIsNeverNull()
        {
            var source = new SubscriptionDataSource();
            var musicServiceInfo = source.GetServiceInformation(Subscriptions.Models.ServiceName.MUSIC);
            Assert.NotNull(musicServiceInfo);
            Assert.Equal(Subscriptions.Models.ServiceName.MUSIC, musicServiceInfo.ServiceName);
        }
    }
}
