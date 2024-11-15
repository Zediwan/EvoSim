import random

class OrganismSettings():
    def get_body_size() -> float:
        UPPER_BOUND = 1.0
        LOWER_BOUND = 0.1
        return random.uniform(LOWER_BOUND, UPPER_BOUND)
    
    SIZE_HEALTH_BASE = 100
    SIZE_HEALTH_OFFSET = 1
    SIZE_HEALTH_SCALING = 1
    SIZE_HEALTH_EXPONENT = 1
    
    SIZE_ENERGY_BASE = 100 
    SIZE_ENERGY_OFFSET = 1
    SIZE_ENERGY_SCALING = 1
    SIZE_ENERGY_EXPONENT = 1
    
    STARTING_HEALTH_FACTOR = 1 # 1 means 100% of MAX_HEALTH
    STARTING_ENERGY_FACTOR = 1 # 1 means 100% of MAX_ENERGY