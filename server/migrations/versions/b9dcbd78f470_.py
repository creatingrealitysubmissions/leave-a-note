"""Change colour column to an int

Revision ID: b9dcbd78f470
Revises: 264a1166d32d
Create Date: 2018-03-14 15:21:00.229962

"""
from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision = 'b9dcbd78f470'
down_revision = '264a1166d32d'
branch_labels = None
depends_on = None


def upgrade():
    op.alter_column(table_name='note', column_name='colour', nullable=False, type_=sa.types.Integer, postgresql_using="0")


def downgrade():
    op.alter_column(table_name='note', column_name='colour', nullable=True, type_=sa.types.String)
