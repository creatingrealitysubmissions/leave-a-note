from flask import Flask, request, jsonify, abort
from models import init_app as init_models, Note, db
from datetime import datetime, timedelta

app = Flask(__name__)
init_models(app)


def note_to_json(note: Note):
    return {'id': note.id, 'kind': note.kind, 'content': note.content, 'lat': note.lat,
            'lon': note.long, 'altitude': note.altitude, 'expires': note.expires,
            'color': note.colour}


@app.route('/notes', methods=['GET'])
def note_list():
    long = float(request.args['long'])
    lat = float(request.args['lat'])
    resolution = float(request.args.get('resolution', '0.001'))
    # TODO: this will fail opposite the prime meridian and/or at the poles
    notes = Note.query.filter(Note.long >= long - resolution, Note.long <= long + resolution,
                              Note.lat >= lat - resolution, Note.lat <= Note.lat + resolution).all()
    return jsonify(notes=[note_to_json(x) for x in notes])


@app.route('/notes', methods=['POST'])
def add_note():
    lon = float(request.form['long'])
    lat = float(request.form['lat'])
    altitude = float(request.form['altitude']) if 'altitude' in request.form else None
    kind = 'text'
    content = request.form['content']
    duration = int(request.form['duration'])
    colour = int(request.form.get('color', '0'))
    expiration = datetime.now() + timedelta(seconds=duration)

    note = Note(kind=kind, content=content, lat=lat, long=lon, altitude=altitude, expires=expiration, colour=colour)
    db.session.add(note)
    db.session.commit()
    return jsonify(note=note_to_json(note))


if __name__ == '__main__':
    app.run('127.0.0.1', 5000, debug=True)
