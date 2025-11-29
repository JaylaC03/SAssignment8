using Geometry.Domain.CubeModel;

namespace Geometry.Application;

public class CubeService
{
    private ICubeRepository _cubeRepository;

    public CubeService(ICubeRepository cubeRepository)
    {
        _cubeRepository = cubeRepository;
    }

    public async Task<Guid> Insert(Cube cube)
    {
        return await _cubeRepository.Insert(cube);
    }

    public async Task<Cube?> ReadById(Guid id)
    {
        return await _cubeRepository.ReadById(id);
    }
}
