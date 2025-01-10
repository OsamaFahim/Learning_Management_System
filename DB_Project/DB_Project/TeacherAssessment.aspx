<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeacherAssessment.aspx.cs" Inherits="TeacherAssessment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://www.google.com/recaptcha/enterprise.js?render=6LcbzMolAAAAAItGp3lXcFkrnfLRY1-l9h-EqCzx"></script>
<script>
grecaptcha.enterprise.ready(function() {
    grecaptcha.enterprise.execute('6LcbzMolAAAAAItGp3lXcFkrnfLRY1-l9h-EqCzx', {action: 'login'}).then(function(token) {
    });
</script>
    <title>Teachers Assessment Form</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Roboto;
            background-color: #FFE0BD;
            font-size: 20px;
        }
        
        .header {
            padding: 20px;
            background-color: #3F51B5;
            color: white;
        }
        
        .date-section {
            margin: 20px;
        }
        
        .date-row,
        .name-row,
        .subject-row {
            display: flex;
            align-items: center;
        }
        
        .date-row input[type="date"],
        .name-row input[type="text"],
        .subject-row input[type="text"] {
            margin-left: 10px;
            width: 150px;
        }
        
        .red-text {
            color: red;
        }
        #Schedule {
            width: 173px;
        }

        .form-container {
            width: 80%;
            margin: 0 auto;
        }

        .criteria-section {
            margin-bottom: 20px;
        }

        .criteria-section table {
            width: 100%;
            border-collapse: collapse;
        }

        .criteria-section th,
        .criteria-section td {
            border: 1px solid #000;
            text-align: center;
            padding: 5px;
        }

        
        input[type="text"],
        input[type="date"],
        textarea {
            font-size: 16px;
            border: 2px solid #999;
            border-radius: 5px;
            padding: 5px;
        }

        textarea {
            width: 100%;
            height: 200px;
            resize: vertical;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="24pt" ForeColor="White" Text="Teachers Assessment Form"></asp:Label>
        </div>
        <div class="date-section">
            <p>
                <asp:Label ID="Label2" runat="server" Text="Date" ForeColor="Red"></asp:Label>
            </p>
            <div class="date-row">
                <p class="red-text">
                    mm-dd-yyyy
                </p>
                <input type="date" id="datePicker" runat="server" />
            </div>
            <p>
                <asp:Label ID="Label4" runat="server" Text="Date" ForeColor="Red"></asp:Label>
            </p>
            <p>
                <asp:Label runat="server" Text="Name of Teacher" ForeColor="Red" ID="ctl11"></asp:Label>
            </p>
            <div class="name-row">
                &nbsp;&nbsp;
                <input type="text" id="FirstName" runat="server" />
                <input type="text" id="LastName" runat="server" />
            </div>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="First Name" ID="FirstNameLabel" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="Last Name" ID="LastNameLabel" style="margin-left: 20px;" ForeColor="Red"></asp:Label>
            </p>
        </div>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="Subject" ID="SubjectLabel" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="Schedule" ID="ScheduleLabel" style="margin-left: 20px;" ForeColor="Red"></asp:Label>
                </p>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="text" id="Subject" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="text" id="Schedule" runat="server" /></p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="Room Number"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="School Year"></asp:Label>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>


      <div class="instructions">
        <p>&nbsp;</p>
          <p>Please fill out the form in evaluating your instructor for the semester. After completion, please press the submit button.</p>
        <p>For reference, the metric are as follows:</p>
        <ol>
            <li>Poor</li>
            <li>Below Average</li>
            <li>Average</li>
            <li>Good</li>
            <li>Excellent</li>
        </ol>
          <p>
              &nbsp;</p>
          <p>
              &nbsp;</p>
          <p>
              &nbsp;</p>
          <p>
              &nbsp;</p>
    </div>

        <!-- Appearence and personal Presentation -->

    <div class="criteria-section">
    <h3>Appearance and Personal Presentation*</h3>
    <table>
        <tr>
            <th>        </th>
            <th>1</th>
            <th>2</th>
            <th>3</th>
            <th>4</th>
            <th>5</th>
        </tr>
        <% string[] criteria = new string[]
        {
            "Teacher attends class in a well presentable manner",
            "Teacher shows enthusiasm when teaching in class",
            "Teacher shows initiative and flexibility in teaching",
            "Teacher is well articulated and shows full knowledge of the subject in teaching",
            "Teacher speaks loud and clear enough to be heard by the whole class"
        };
        for (int i = 0; i < criteria.Length; i++)
        { %>
            <tr>
                <td><%= criteria[i] %></td>
                <% for (int j = 1; j <= 5; j++)
                { %>
                    <td>
                        <input type="radio" name="criteria<%= i %>" value="<%= j %>" runat="server" />
                    </td>
                <% } %>
            </tr>
        <% } %>
    </table>
</div>


    <!-- Professional practices -->
<div class="criteria-section">
    <h3>Professional Practices*</h3>
    <table>
        <tr>
            <th>       </th>
            <th>1</th>
            <th>2</th>
            <th>3</th>
            <th>4</th>
            <th>5</th>
        </tr>
        <% string[] professionalPractices = new string[]
        {
            "Teacher shows professionalism in class",
            "Teacher shows commitment to teaching the class",
            "Teacher encourages students to engage in class discussions and ask questions",
            "Teacher handles criticisms and suggestions professionally",
            "Teacher comes to class on time",
            "Teacher ends class on time"
        };
        for (int i = 0; i < professionalPractices.Length; i++)
        { %>
            <tr>
                <td><%= professionalPractices[i] %></td>
                <% for (int j = 1; j <= 5; j++)
                { %>
                    <td>
                        <input type="radio" name="professionalPractices<%= i %>" value="<%= j %>" runat="server" />
                    </td>
                <% } %>
            </tr>
        <% } %>
    </table>
</div>

    <!-- Teaching methods -->
<div class="criteria-section">
    <h3>Teaching Methods*</h3>
    <table>
        <tr>
            <th>       </th>
            <th>1</th>
            <th>2</th>
            <th>3</th>
            <th>4</th>
            <th>5</th>
        </tr>
        <% string[] teachingMethods = new string[]
        {
            "Teacher shows well rounded knowledge over the subject matter",
            "Teacher has organized the lesson conducive for easy understanding of students",
            "Teacher shows dynamism and enthusiasm",
            "Teacher stimulates the critical thinking of students",
            "Teacher handles the class environment conducive for learning"
        };
        for (int i = 0; i < teachingMethods.Length; i++)
        { %>
            <tr>
                <td><%= teachingMethods[i] %></td>
                <% for (int j = 1; j <= 5; j++)
                { %>
                    <td>
                        <input type="radio" name="teachingMethods<%= i %>" value="<%= j %>" runat="server" />
                    </td>
                <% } %>
            </tr>
        <% } %>
    </table>
</div>

    <!-- Dispostion towards students  -->
<div class="criteria-section">
    <h3>Disposition Towards Students*</h3>
    <table>
        <tr>
            <th>       </th>
            <th>1</th>
            <th>2</th>
            <th>3</th>
            <th>4</th>
            <th>5</th>
        </tr>
        <% string[] dispositionTowardsStudents = new string[]
        {
            "Teacher believes that students can learn from the class",
            "Teacher shows equal respect to various cultural backgrounds, individuals, religion, and race",
            "Teacher finds the students strengths as basis for growth and weaknesses for opportunities for improvement",
            "Teacher understands the weakness of a student and helps in the student's improvement"
        };
        for (int i = 0; i < dispositionTowardsStudents.Length; i++)
        { %>
            <tr>
                <td><%= dispositionTowardsStudents[i] %></td>
                <% for (int j = 1; j <= 5; j++)
                { %>
                    <td>
                        <input type="radio" name="dispositionTowardsStudents<%= i %>" value="<%= j %>" runat="server" />
                    </td>
                <% } %>
            </tr>
        <% } %>
    </table>

</div>


        <p>
            &nbsp;</p>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" ForeColor="Black" Text="Comments"></asp:Label>
        </p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox3" runat="server" Height="177px" Width="368px"></asp:TextBox>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" BackColor="#999999" BorderColor="#999999" ForeColor="Black" OnClick="Button1_Click" Text="Submit" />


    </form>


      <p>
          &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>


</body>
</html>
