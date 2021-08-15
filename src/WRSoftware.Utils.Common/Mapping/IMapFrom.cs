using AutoMapper;

namespace WRSoftware.Utils.Common.Mapping
{
    /// <summary>
    /// Generic interface that should be inherited/implemented
    /// by class that has a DTO, thus facilitanting the mapping between
    /// theses objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapFrom<T>
    {
        /// <summary>
        /// Mappings the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}