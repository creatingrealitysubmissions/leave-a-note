from flask import Flask
from models import init_app as init_models

app = Flask(__name__)
init_models(app)


@app.route('/')
def index():
    return 'Hello there!'


if __name__ == '__main__':
    app.run('127.0.0.1', 5000)
