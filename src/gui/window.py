import tkinter as tk

class MainWindow:
    def __init__(self, title="PyEvoSim", width=800, height=600):
        self.root = tk.Tk()
        self.root.title(title)
        self.root.geometry(f"{width}x{height}")

    def run(self):
        self.root.mainloop()