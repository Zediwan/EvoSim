from __future__ import annotations
import numpy as np

class NeuralNetwork:
    def __init__(self, input_size, hidden_size, output_size, mutation_chance=0.01, mutation_rate=0.1):
        self.input_size = input_size
        self.hidden_size = hidden_size
        self.output_size = output_size
        self.mutation_chance = mutation_chance
        self.mutation_rate = mutation_rate
        self.weights_input_hidden = np.random.randn(input_size, hidden_size)
        self.weights_hidden_output = np.random.randn(hidden_size, output_size)

    def forward(self, inputs):
        hidden = np.dot(inputs, self.weights_input_hidden)
        hidden = self.sigmoid(hidden)
        output = np.dot(hidden, self.weights_hidden_output)
        output = self.sigmoid(output)
        return output

    def sigmoid(self, x):
        return 1 / (1 + np.exp(-x))

    def mutate(self):
        def mutate_weights(weights):
            for i in range(weights.shape[0]):
                for j in range(weights.shape[1]):
                    if np.random.rand() < self.mutation_chance:
                        weights[i, j] += np.random.randn() * self.mutation_rate

        mutate_weights(self.weights_input_hidden)
        mutate_weights(self.weights_hidden_output)

    def copy(self) -> NeuralNetwork:
        new_nn = NeuralNetwork(
            self.input_size,
            self.hidden_size,
            self.output_size,
            self.mutation_chance,
            self.mutation_rate
        )
        new_nn.weights_input_hidden = np.copy(self.weights_input_hidden)
        new_nn.weights_hidden_output = np.copy(self.weights_hidden_output)
        return new_nn