import React from 'react'

const New = () => {
  return (
    <div className="modal-container">
      <div className="modal-title">New Appointment</div>

      <div className="mt-15">
        <label htmlFor="Title_n">Title</label> <br />
        <input type="text" id="Title_n" maxLength={150} name="title"/>
        <span className="ms-10">0/150</span>
      </div>

      <div className="mt-15">
        <label htmlFor="Description_n">Description</label> <br />
        <input type="text" id="Description_n" maxLength={150} name="description"/>
      </div>

      <div className="row mt-15">
        <div>
          <label htmlFor="Address_n">Address</label> <br />
          <input type="text" id="Address_n" maxLength={100} name="address"
            // onChange={newData}
          />
        </div>

        <div className="ms-10">
          <label htmlFor="LevelOfImportance_f">Importance</label> <br />
          <select name="LevelOfImportance" id="LevelOfImportance_f"
            defaultValue={"2"}
            // onChange={newData}
          >
            <option value="5">Ver High</option>
            <option value="4">High</option>
            <option value="3">Medium</option>
            <option value="2">Normal</option>
            <option value="1">Low</option>
            <option value="0">Very Low</option>
          </select>
        </div>
      </div>


    </div>
  )
}

export default New