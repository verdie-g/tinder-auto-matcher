using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tinder.Models;

namespace Tinder
{
    public interface ITinderClient
    {
        /// <summary>
        /// Get your matches.
        /// </summary>
        /// <param name="count">Max number of matches to return.</param>
        /// <param name="isTinderU">???</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task<IReadOnlyList<Match>> GetMatches(int count = 60, bool isTinderU = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get your metadatas.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task<Meta> GetMetadatas(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get your own profile.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task<Profile> GetProfile(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get recommendations for you or null if there is none.
        /// </summary>
        Task<IReadOnlyList<Recommendation>> GetRecommendations(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get teaser preview (number of people who liked you + last person who liked you).
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task<TeaserData> GetTeaser(CancellationToken cancellationToken = default);

        /// <summary>
        /// Get pictures of the ten last people who liked you.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task<IReadOnlyList<Teaser>> GetTeasers(CancellationToken cancellationToken = default);

        /// <summary>
        /// Like an user (swipe right).
        /// </summary>
        /// <param name="userId">Id of the target user.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task<Like> Like(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Disike an user (swipe left).
        /// </summary>
        /// <param name="userId">Id of the target user.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task Pass(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Message an user. 
        /// </summary>
        /// <param name="userId">Id of the target user.</param>
        /// <param name="message">Message content.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task Message(string userId, string message, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update your location.
        /// </summary>
        /// <param name="geolocation">New location.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task Ping(Geolocation geolocation, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update your location (Tinder Plus Only).
        /// </summary>
        /// <param name="geolocation">New location.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task Travel(Geolocation geolocation, CancellationToken cancellationToken = default);

        /// <summary>
        /// Unmatch an user.
        /// </summary>
        /// <param name="match">Id of the target user.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        Task Unmatch(string matchId, CancellationToken cancellationToken = default);
    }

}