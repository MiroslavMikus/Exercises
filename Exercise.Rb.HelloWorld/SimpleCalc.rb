puts "Enter operator:"
op = gets.chomp()

def getNumber (header)
    puts "Enter " + header.to_s() + ". number:"
    return gets.to_f()
end

if op == "+"
    puts (getNumber(1) + getNumber(2)).to_s()
elsif op == "-"
    puts (getNumber(1) - getNumber(2)).to_s()
elsif op == "/"
    puts (getNumber(1) / getNumber(2)).to_s()
elsif op == "*"
    puts (getNumber(1) * getNumber(2)).to_s()
end


puts "Press enter to exit"
gets

