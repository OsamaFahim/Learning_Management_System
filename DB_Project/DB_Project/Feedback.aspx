<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" %>

<!DOCTYPE html>
<html>
<head>
    <title>Feedback Form</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        body {
            background-color: #f5f5f5;
        }

        h1 {
            font-family: 'Roboto', sans-serif;
            color: #4CAF50;
            margin-bottom: 30px;
            font-size:50px;
        }

        .container {
            background-color: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0px 0px 10px 0px rgba(0,0,0,0.1);
        }

          #lblErrorMessage {
               color: darkred; /* light green color */
               font-size: 1.2em; /* increase font size */
                 font-weight: bold; /* make the text bold */
                text-align: center; /* center align the text */
                 margin: 10px 0; /* add some margin around the label */
              margin-top: 10px;
         }


        .form-check-label {
            margin-right: 20px;
        }

       .btn-primary {
            background-color: #4CAF50;
            border-color: #4CAF50;
        }

    </style>
</head>
<body>
    <div class ="container">
    <div class="container mt-5">
        <h1 class="text-center">Feedback</h1>
        <form runat ="server">
          <div class="select-Faculty">
                <table>
                    <tr>
                        <td>Select Course Name</td>
                        <td>
                          
                         <asp:DropDownList ID="CourseID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CourseID_SelectedIndexChanged"></asp:DropDownList>

                    </td>
                  
                </tr>
                </table>
            </div>

            <br />

            <div class="form-group">
    <label>Quality</label>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching1" id="excellent1" value="excellent">
        <label class="form-check-label" for="excellent1">Excellent</label>
    </div>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching1" id="average1" value="average">
        <label class="form-check-label" for="average1">Average</label>
    </div>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching1" id="poor1" value="poor">
        <label class="form-check-label" for="poor1">Poor</label>
    </div>
</div>

<div class="form-group">
    <label>Grading</label>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching2" id="excellent2" value="excellent">
        <label class="form-check-label" for="excellent2">Excellent</label>
    </div>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching2" id="average2" value="average">
        <label class="form-check-label" for="average2">Average</label>
    </div>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching2" id="poor2" value="poor">
        <label class="form-check-label" for="poor2">Poor</label>
    </div>
</div>

<div class="form-group">
    <label>Communication in Class</label>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching3" id="excellent3" value="excellent">
        <label class="form-check-label" for="excellent3">Excellent</label>
    </div>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching3" id="average3" value="average">
        <label class="form-check-label" for="average3">Average</label>
    </div>
    <div class="form-check">
        <input class="form-check-input" type="radio" name="qualityOfTeaching3" id="poor3" value="poor">
        <label class="form-check-label" for="poor3">Poor</label>
    </div>
</div>

           <div class="form-group">
    <label for="review">Review</label>
    <textarea class="form-control" runat="server" ID="review" rows="3"></textarea>
    </div>

              <div >
                <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
            </div>


            <div class="submit-button">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
            </div>
        </form>
    </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
