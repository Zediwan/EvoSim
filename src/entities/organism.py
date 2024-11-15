from settings import OrganismSettings

class Organism:
    body_size: float
    
    def __init__(self) -> None:
        self.body_size = OrganismSettings.get_size()
        self.health = self.MAX_HEALTH * OrganismSettings.STARTING_HEALTH_FACTOR
        self.energy = self.MAX_ENERGY * OrganismSettings.STARTING_ENERGY_FACTOR
    
    def move(self) -> None:
        desired_direction = self.think()
    
    def think(self) -> tuple[float, float]:
        return (0, 0)
    
    def eat(self) -> None:
        pass
    
    def reproduce(self) -> None:
        pass
        
    @property
    def MAX_HEALTH(self) -> float:
        return OrganismSettings.SIZE_HEALTH_BASE + (((OrganismSettings.SIZE_HEALTH_OFFSET + self.body_size) * OrganismSettings.SIZE_HEALTH_SCALING) ** OrganismSettings.SIZE_HEALTH_EXPONENT)
    
    @property
    def MAX_ENERGY(self) -> float:
        return OrganismSettings.SIZE_ENERGY_BASE + (((OrganismSettings.SIZE_ENERGY_OFFSET + self.body_size) * OrganismSettings.SIZE_ENERGY_SCALING) ** OrganismSettings.SIZE_ENERGY_EXPONENT)
    