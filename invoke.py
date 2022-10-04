from flask import Flask, jsonify, request

from xml import lambda_handler

app = Flask(__name__)


@app.route("/aws-endpoint", methods=['POST'])
def hello_world():
    request_json = request.get_json()
    event = {'body': request_json}

    result = lambda_handler(event, None)
    result = {"id": "1",
              "Topic": result}
    return result


if __name__ == "__main__":
    app.run()
