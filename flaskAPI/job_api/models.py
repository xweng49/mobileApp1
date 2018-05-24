from run import db
from passlib.hash import pbkdf2_sha256 as sha256
from datetime import datetime
from flask import jsonify

class UserModel(db.Model):
	__tablename__ = "users"

	id = db.Column(db.Integer, primary_key = True)
	username = db.Column(db.String(120), unique = True, nullable = False)
	password = db.Column(db.String(120), nullable = False)
	firstname = db.Column(db.String(120), nullable = False)
	lastname = db.Column(db.String(120), nullable = False)

	def save_to_db(self):
		db.session.add(self)
		db.session.commit()

	@classmethod
	def find_by_username(cls, username):
		return cls.query.filter_by(username = username).first()



class JobModel(db.Model):
	__tablename__ = 'jobs'

	id = db.Column(db.Integer, primary_key = True)
	firstname = db.Column(db.String(120))
	lastname = db.Column(db.String(120))
	address = db.Column(db.String(120))
	description = db.Column(db.String(120))
	complete = db.Column(db.String(120))
	time = db.Column(db.String(120))
	assignedTime = db.Column(db.String(120))
	assignedTo = db.Column(db.String(120))



	def add(self):
		db.session.add(self)
		db.session.commit()


	@classmethod
	def return_all(cls):
		def to_json(x):
			return {
				'firstname':x.firstname,
				'lastname':x.lastname,
				'address':x.address,
				'description':x.description,
				'complete':x.complete,
				'time':x.time,
				'assignedTime':x.assignedTime,
				'assignedTo':x.assignedTo
			}

		return list(map(lambda x: to_json(x), JobModel.query.all()))