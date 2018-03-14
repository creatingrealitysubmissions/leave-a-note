from flask_sqlalchemy import SQLAlchemy
from flask_migrate import Migrate
from sqlalchemy.dialects.postgresql import DOUBLE_PRECISION
from os import environ

db = SQLAlchemy()
migrate = Migrate(db)


class Note(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    kind = db.Column(db.String, nullable=False)
    content = db.Column(db.Text)
    # We could use PostGIS for this, but I think it's overkill when we just need to find points.
    lat = db.Column(DOUBLE_PRECISION, nullable=False)
    long = db.Column(DOUBLE_PRECISION, nullable=False)
    altitude = db.Column(db.Float)
    expires = db.Column(db.DateTime, index=True)
    colour = db.Column(db.String)
db.Index('note_lat_long_index', Note.lat, Note.long)


def init_app(app):
    app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
    app.config['SQLALCHEMY_DATABASE_URI'] = environ['DATABASE_URL']
    db.init_app(app)
    migrate.init_app(app, db)
