from flask_restful import Resource, reqparse
from models import UserModel, JobModel
from flask_jwt_extended import (create_access_token, create_refresh_token, jwt_required, jwt_refresh_token_required, get_jwt_identity, get_raw_jwt)
from flask import jsonify, json
import requests
import datetime


registerParser = reqparse.RequestParser()
registerParser.add_argument('username', help = 'Username cannot be blank', required = True)
registerParser.add_argument('firstname', help = 'First Name cannot be blank', required = True)
registerParser.add_argument('lastname', help = 'Last Name cannot be blank', required = True)
registerParser.add_argument('password', help = 'Password cannot be blank', required = True)

loginParser = reqparse.RequestParser()
loginParser.add_argument('username', help = 'Username cannot be blank', required = True)
loginParser.add_argument('password', help = 'Password cannot be blank', required = True)

addJobParser = reqparse.RequestParser()
addJobParser.add_argument('firstname', help = 'Username cannot be blank', required = True)
addJobParser.add_argument('lastname', help = 'Username cannot be blank', required = True)
addJobParser.add_argument('address', help = 'Address cannot be blank', required = True)
addJobParser.add_argument('description', help = 'Username cannot be blank', required = True)
addJobParser.add_argument('complete', help = 'Username cannot be blank', required = True)
addJobParser.add_argument('time', help = 'Username cannot be blank', required = True)
addJobParser.add_argument('assignedTime', help = 'Username cannot be blank', required = True)
addJobParser.add_argument('assignedTo', help = 'Username cannot be blank', required = True)


class UserRegistration(Resource):
	def post(self):
		data = registerParser.parse_args()

		if UserModel.find_by_username(data['username']):
			return {'message': 'User {} already exists'. format(data['username'])}

		new_user = UserModel(
			username = data['username'],
			password = data['password'],
			firstname = data['firstname'],
			lastname = data['lastname']
		)

		try:
			new_user.save_to_db()

			return {
				'message': 'User {} was created'.format(data['username'])
			}
		except:
			return {'message': 'Something went wrong'}, 500

class UserLogin(Resource):
	def post(self):
		data = loginParser.parse_args()
		current_user = UserModel.find_by_username(data['username'])
		if not current_user:
			return {'message': 'User {} doesn\'t exist'.format(data['username'])}

		if data['password'] == current_user.password:
			return {
			'message': 'Logged in as {}'. format(current_user.username)
			}
		else:
			return {'message': 'Wrong credentials'}

class AddJob(Resource):
	def post(self):
		data = addJobParser.parse_args()

		new_job = JobModel(
			firstname = data['firstname'],
			lastname = data['lastname'],
			address = data['address'],
			description = data['description'],
			complete = data['complete'],
			time = data['time'],
			assignedTime = data['assignedTime'],
			assignedTo = data['assignedTo']
		)

		try:
			new_job.add()

			return {'message': 'Job at location {} was created'.format(data['address'])}

		except:
			return {'message': 'Error saving job'}

class GetJobs(Resource):
	def get(self):
		return JobModel.return_all()