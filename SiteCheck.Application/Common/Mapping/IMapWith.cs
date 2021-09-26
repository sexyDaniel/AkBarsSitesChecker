

using AutoMapper;

namespace SiteCheck.Application.Common.Mapping
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile);
    }
}
